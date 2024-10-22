using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class ProdutoGeralDomainService : IProdutoGeralDomainService
    {
        private readonly IProdutoGeralRepository? _produtoGeralRepository;  

        private readonly IVendaProdutoGeralRepository? _vendaProdutoGeralRepository;
        private readonly IProdutoDepositoRepository? _produtoDepositoRepository;
        private readonly IDepositosRepository? _depositosRepository;

        public ProdutoGeralDomainService(IProdutoGeralRepository? produtoGeralRepository,

            IVendaProdutoGeralRepository? vendaProdutoGeralRepository,
            IProdutoDepositoRepository? produtoDepositoRepository,
            IDepositosRepository? depositosRepository)
        {
            _produtoGeralRepository = produtoGeralRepository;

            _vendaProdutoGeralRepository = vendaProdutoGeralRepository;
            _produtoDepositoRepository = produtoDepositoRepository;
            _depositosRepository = depositosRepository;
        }



        public ProdutoGeral Atualizar(ProdutoGeral produtoGeral, string matricula)
        {
            Console.WriteLine($"Atualizando Produto ID: {produtoGeral.Id}, Nome: {produtoGeral.NomeProduto}");
            Console.WriteLine($"Quantidade de depósitos recebidos: {produtoGeral.ProdutoDeposito?.Count}");

            // Verifica se o produto já existe no banco de dados
            var registro = ObterPorId(produtoGeral.Id);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para atualização.");

            // Verifica se há depósitos disponíveis
            if (!produtoGeral.ProdutoDeposito.Any())
            {
                throw new ApplicationException("Nenhum depósito disponível para atualizar.");
            }

            // Inicializa uma lista para os depósitos que vão ser atualizados ou criados
            var depositosAtualizados = new List<ProdutoDeposito>();

            foreach (var deposito in produtoGeral.ProdutoDeposito)
            {
                Console.WriteLine($"Processando depósito: DepositoId = {deposito.DepositoId}, Quantidade = {deposito.Quantidade}");

                // Verificar se o depósito já existe no banco
                var depositoExistente = _produtoDepositoRepository.ObterPorIdProduto(deposito.DepositoId, produtoGeral.Id);

                if (depositoExistente != null)
                {
                    // Se o depósito já está associado, apenas atualiza a quantidade
                    depositoExistente.Quantidade =  deposito.Quantidade;
                    depositoExistente.ProdutoGeralId = produtoGeral.Id;
                    depositoExistente.NomeProduto = produtoGeral.NomeProduto;
                    depositoExistente.CodigoSistema = produtoGeral.CodigoSistema;
                    depositoExistente.DepositoId = deposito.DepositoId; // Usar DepositoId
                    depositoExistente.NomeDeposito = depositoExistente.NomeDeposito; // Preservando o nome existente

                    Console.WriteLine($"Atualizando depósito existente: DepositoId = {depositoExistente.DepositoId}, Nova Quantidade = {depositoExistente.Quantidade}");
                    _produtoDepositoRepository.Update(depositoExistente);
                    depositosAtualizados.Add(depositoExistente);
                }
                else
                {
                    // Verifica se o depósito existe na tabela de depósitos
                    var depositoEntidade = _depositosRepository.GetById(deposito.DepositoId);
                    if (depositoEntidade == null)
                    {
                        throw new ArgumentException($"Depósito com ID {deposito.Id} não encontrado.");
                    }

                    // Se não estiver associado, cria um novo depósito
                    var novoDeposito = new ProdutoDeposito
                    {
                        ProdutoGeralId = produtoGeral.Id,
                        DepositoId = deposito.DepositoId,
                        Quantidade = deposito.Quantidade,
                        NomeProduto = produtoGeral.NomeProduto,
                        CodigoSistema = produtoGeral.CodigoSistema,
                        NomeDeposito = depositoEntidade.Nome, // Preenche o nome do depósito corretamente
                    };

                    Console.WriteLine($"Criando novo depósito: DepositoId = {novoDeposito.DepositoId}, Quantidade = {novoDeposito.Quantidade}");
                    _produtoDepositoRepository.Add(novoDeposito); // Salvar o novo depósito
                    depositosAtualizados.Add(novoDeposito);
                }
            }


            var produtoAtualizado = new ProdutoGeral
            {
                Id = produtoGeral.Id,
                NomeProduto = produtoGeral.NomeProduto,
                MarcaProduto = produtoGeral.MarcaProduto,
                Un = produtoGeral.Un,
                CodigoSistema = produtoGeral.CodigoSistema,
                ImagemUrlGeral = registro.ImagemUrlGeral,
                DataHoraAlteracao = DateTime.Now,
                DataHoraCadastro = registro.DataHoraCadastro,
                QuantidadeProd = depositosAtualizados.Sum(d => d.Quantidade),
                FornecedorGeralId = registro.FornecedorGeralId,
                CategoriaId = registro.CategoriaId,
            };

            if (registro.DataHoraCadastro == null)
            {
                produtoAtualizado.DataHoraCadastro = DateTime.Now;
            }



            
            _produtoGeralRepository.Update(produtoAtualizado);
            Console.WriteLine($"Produto atualizado: {produtoAtualizado.NomeProduto}, Nova Quantidade Total = {produtoAtualizado.QuantidadeProd}");

            // Retorna o produto atualizado
            return _produtoGeralRepository?.GetById(produtoGeral.Id);
        }









        public ProdutoGeral Cadastrar(ProdutoGeral produtoGeral, List<(int depositoId, int quantidade)> depositosSelecionados, string matricula)
        {
            if (produtoGeral == null)
                throw new ArgumentNullException(nameof(produtoGeral));

            if (depositosSelecionados == null || !depositosSelecionados.Any())
                    throw new ArgumentException("A lista de depósitos não pode estar vazia.");

            try
            {
                // **Primeira correção**: Salva o ProdutoGeral primeiro para garantir que o ProdutoGeralId existe
                _produtoGeralRepository.Add(produtoGeral);


                // Agora que o ProdutoGeral foi salvo, podemos usar o produtoGeral.Id
                // Inicializa a lista de ProdutoDepositos se não estiver inicializada
                if (produtoGeral.ProdutoDeposito == null)
                    produtoGeral.ProdutoDeposito = new List<ProdutoDeposito>();

                // Para cada depósito selecionado, distribua a quantidade
                foreach (var (depositoId, quantidade) in depositosSelecionados)
                {
                    // Verificar se o depósito já está associado ao produto
                    var depositoExistente = produtoGeral.ProdutoDeposito
                        .FirstOrDefault(pd => pd.DepositoId == depositoId);

                    if (depositoExistente != null)
                    {
                        // Se o depósito já está associado, apenas atualiza a quantidade
                        depositoExistente.Quantidade += quantidade;
                        _produtoDepositoRepository.Update(depositoExistente);
                    }
                    else
                    {
                        // Buscar o nome do depósito usando o repositório de depósitos
                        var deposito = _depositosRepository.GetById(depositoId);
                        if (deposito == null)
                        {
                            throw new ArgumentException($"Depósito com ID {depositoId} não encontrado.");
                        }

                        // Se não estiver associado, crie a relação com a quantidade
                        var novoProdutoDeposito = new ProdutoDeposito
                        {
                            ProdutoGeralId = produtoGeral.Id, // ProdutoGeralId agora está definido corretamente
                            DepositoId = depositoId,
                            Quantidade = quantidade,
                            NomeDeposito = deposito.Nome, // Preenche o nome do depósito corretamente
                            CodigoSistema = produtoGeral.CodigoSistema,
                            NomeProduto = produtoGeral.NomeProduto,
                        };
                        produtoGeral.ProdutoDeposito.Add(novoProdutoDeposito);
                        _produtoDepositoRepository.Add(novoProdutoDeposito);
                    }
                }

                // Calcula a quantidade total do produto baseado nos depósitos
                produtoGeral.QuantidadeProd = produtoGeral.ProdutoDeposito.Sum(pd => pd.Quantidade);

                // **Salva o produto e os depósitos associados**
                _produtoGeralRepository.Update(produtoGeral); // Agora atualiza o ProdutoGeral com as associações de depósitos
                produtoGeral = _produtoGeralRepository.GetById(produtoGeral.Id);

                // Retorna o produto após ser salvo
                return produtoGeral;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar o produto: {ex}");
                throw; // Re-throw para que a exceção seja tratada no nível superior
            }
        }






        public void ConfirmarVenda(int depositoId, int quantidadeVendida, string matricula)
        {
            // Obtém o ProdutoDeposito pelo ID do depósito, incluindo a entidade ProdutoGeral
            var produtoDeposito = _produtoDepositoRepository.ObterId(depositoId);

            if (produtoDeposito == null)
                throw new ApplicationException("ProdutoDepósito não encontrado.");

            if (produtoDeposito.Quantidade < quantidadeVendida)
                throw new ApplicationException("Quantidade insuficiente no depósito.");

            // Verifica se ProdutoGeral está associado corretamente
            if (produtoDeposito.ProdutoGeral == null)
                throw new ApplicationException("ProdutoGeral não associado ao ProdutoDeposito.");

            // Cria a venda associada ao ProdutoDeposito correto
            var venda = new VendaProdutoGeral
            {
                ProdutoDepositoId = produtoDeposito.Id,
                QuantidadeVendida = quantidadeVendida,
                DataVendaManual = DateTime.Now,
                UsuarioId = matricula,
                CodigoSistema = produtoDeposito.CodigoSistema,
                NomeProduto = produtoDeposito.NomeProduto,
                Marca = produtoDeposito.ProdutoGeral.MarcaProduto, // Obtendo a marca corretamente
                NomeDeposito = produtoDeposito.NomeDeposito,
            };

            // Adiciona a venda no repositório
            _vendaProdutoGeralRepository.Add(venda);

            // Atualiza a quantidade de produtos no depósito
            produtoDeposito.Quantidade -= quantidadeVendida;
            _produtoDepositoRepository.Update(produtoDeposito);

            // Atualiza o ProdutoGeral correspondente
            var produtoGeral = _produtoGeralRepository.GetById(produtoDeposito.ProdutoGeralId);

            if (produtoGeral == null)
                throw new ApplicationException("ProdutoGeral não encontrado.");

            // Recalcula a quantidade total do ProdutoGeral com base nos depósitos associados
            produtoGeral.QuantidadeProd = _produtoDepositoRepository
                .GetByProdutoGeralId(produtoDeposito.ProdutoGeralId)
                .Sum(p => p.Quantidade);

            // Atualiza o ProdutoGeral
            _produtoGeralRepository.Update(produtoGeral);
        }



        public List<ProdutoGeral> Consultar()
        {
            var produtos = _produtoGeralRepository?.GetAll();

            if (produtos == null)
                return new List<ProdutoGeral>();

            foreach (var produto in produtos)
            {
                produto.QuantidadeProd = produto.ProdutoDeposito.Sum(l => l.Quantidade);
            }

            return produtos;
        }

        public List<ProdutoDeposito> ConsultarQuantidadeProdutoDeposito(int produtoGeralId)
        {
            return _produtoDepositoRepository.GetByProdutoGeralId(produtoGeralId);
        }

        public void ExcluirQuantidadeProdutoDeposito(int produtoGeralId, int quantidadeProdutoDepositoId)
        {
            throw new NotImplementedException();
        }

        public ProdutoGeral Inativar(int id)
        {
            var produtoGeral = ObterPorId(id);

            if (produtoGeral == null)
                throw new ApplicationException("Produto não encontrado para exclusão.");


            produtoGeral.DataHoraAlteracao = DateTime.Now;

            _produtoGeralRepository?.Update(produtoGeral);

            return produtoGeral;
        }
        public void Excluir(int id)
        {
            var produtoGeral = ObterPorId(id); // Buscar o produto pelo id

            if (produtoGeral == null)
                throw new ApplicationException("Produto não encontrado para exclusão.");

            // Remover o produto do repositório
            _produtoGeralRepository?.Delete(produtoGeral);
        }

        public ProdutoGeral ObterPorId(int id)
        {
            var produtoGeral = _produtoGeralRepository?.GetById(id);


            return produtoGeral;
        }


        public void UploadVenda(int id, int quantidadeVendida, string matricula, string dataVenda)
        {
            // Obter o ProdutoDeposito associado ao ID, incluindo ProdutoGeral para garantir o acesso à marca
            var produtoDeposito = _produtoDepositoRepository.ObterId(id);

            if (produtoDeposito == null)
            {
                throw new ApplicationException("Depósito não encontrado.");
            }

            // Validação do depósito com base no nome do depósito obtido do produto
            if (produtoDeposito.NomeDeposito != "JC1" && produtoDeposito.NomeDeposito != "JC2" && produtoDeposito.NomeDeposito != "VA" && produtoDeposito.NomeDeposito != "G8")
            {
                throw new ApplicationException($"Venda não permitida. Produto não pertence a um depósito permitido (JC1, JC2, VA).");
            }

            // Verificar se ProdutoGeral está associado e não é nulo
            if (produtoDeposito.ProdutoGeral == null)
            {
                throw new ApplicationException("ProdutoGeral não associado ao ProdutoDeposito.");
            }

            // Criar o registro de venda
            var venda = new VendaProdutoGeral
            {
                ProdutoDepositoId = produtoDeposito.Id,
                QuantidadeVendida = quantidadeVendida,
                UploadRelatorioVenda = DateTime.Now,
                UsuarioId = matricula,
                CodigoSistema = produtoDeposito.CodigoSistema,
                NomeProduto = produtoDeposito.NomeProduto,
                Marca = produtoDeposito.ProdutoGeral.MarcaProduto, // Obtendo a marca do ProdutoGeral
                DataVenda = dataVenda,
                NomeDeposito = produtoDeposito.NomeDeposito,
            };

            // Registrar a venda
            _vendaProdutoGeralRepository.Add(venda);

            // Atualizar a quantidade no depósito
            produtoDeposito.Quantidade -= quantidadeVendida;
            _produtoDepositoRepository.Update(produtoDeposito);

            // Atualizar a quantidade total do produto geral no sistema
            var produtoGeral = _produtoGeralRepository.GetById(produtoDeposito.ProdutoGeralId);

            if (produtoGeral == null)
            {
                throw new ApplicationException("Produto geral não encontrado.");
            }

            // Atualiza a quantidade total considerando todos os depósitos do produto
            produtoGeral.QuantidadeProd = _produtoGeralRepository.GetProdutosDepositosProdutoId(produtoGeral.Id)
                .Where(p => p.Quantidade >= 0)
                .Sum(p => p.Quantidade);

            // Atualizar o ProdutoGeral
            _produtoGeralRepository.Update(produtoGeral);
        }


    }
}
