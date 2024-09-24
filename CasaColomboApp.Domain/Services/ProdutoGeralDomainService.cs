using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class ProdutoGeralDomainService : IProdutoGeralDomainService
    {
        private readonly IProdutoGeralRepository? _produtoGeralRepository;
        private readonly IQuantidadeProdutosDepositosRepository? _quantidadeProdutosDepositosRepository;
        private readonly IVendaProdutoGeralRepository? _vendaProdutoGeralRepository;

        public ProdutoGeralDomainService(IProdutoGeralRepository? produtoGeralRepository, 
            IQuantidadeProdutosDepositosRepository quantidadeProdutosDepositosRepository,
            IVendaProdutoGeralRepository? vendaProdutoGeralRepository)
        {
            _produtoGeralRepository = produtoGeralRepository;
            _quantidadeProdutosDepositosRepository = quantidadeProdutosDepositosRepository;
            _vendaProdutoGeralRepository = vendaProdutoGeralRepository;
        }



        public ProdutoGeral Atualizar(ProdutoGeral produtoGeral, string matricula)
        {
            var registro = ObterPorId(produtoGeral.Id);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para edição.");

            // Remove os lotes excluídos do produto
            produtoGeral.QuantidadeProdutoDeposito.RemoveAll(l => l.Quantidade == 0 && string.IsNullOrEmpty(l.NomeDeposito));

            // Constrói uma nova lista de lotes para o produto atualizado
            var quantidadeProdutoDepositoAtualizados = new List<QuantidadeProdutosDepositos>();
            foreach (var quantidadeProdutosDepositos in produtoGeral.QuantidadeProdutoDeposito)
            {
                var quantidadeprodutoExistente = _quantidadeProdutosDepositosRepository.ObterPorId(quantidadeProdutosDepositos.Id);

                if (quantidadeprodutoExistente != null)
                {
                    // Se os campos QtdEntrada e NomeProduto não forem fornecidos, mantenha os valores atuais
                    quantidadeprodutoExistente.Quantidade = quantidadeProdutosDepositos.Quantidade;
                    quantidadeprodutoExistente.NomeDeposito = quantidadeProdutosDepositos.NomeDeposito;
                    quantidadeprodutoExistente.NomeProduto = string.IsNullOrEmpty(quantidadeProdutosDepositos.NomeProduto) ? quantidadeprodutoExistente.NomeProduto : quantidadeProdutosDepositos.NomeProduto;
                    quantidadeprodutoExistente.UsuarioId = matricula;
                    quantidadeprodutoExistente.CodigoSistema = quantidadeProdutosDepositos.CodigoSistema;


                    quantidadeprodutoExistente.DataUltimaAlteracao = DateTime.Now;
                  

                    quantidadeProdutoDepositoAtualizados.Add(quantidadeprodutoExistente);
                }
                else
                {
                    // Adiciona um novo lote com status Ativo
                    quantidadeProdutoDepositoAtualizados.Add(new QuantidadeProdutosDepositos
                    {
                        
                        Quantidade = quantidadeProdutosDepositos.Quantidade,
                        CodigoSistema = quantidadeProdutosDepositos.CodigoSistema,
                        NomeProduto = produtoGeral.NomeProduto,
                        DataUltimaAlteracao = DateTime.Now,
                        DataEntrada = DateTime.Now,
                        UsuarioId = matricula,
                        NomeDeposito = quantidadeProdutosDepositos.NomeDeposito
                        
                    });
                }
            }

            // Identifica lotes que foram removidos do produto e devem ser desativados

            var produtoAtualizado = new ProdutoGeral
            {
                Id = produtoGeral.Id,
                NomeProduto = produtoGeral.NomeProduto,
                MarcaProduto = produtoGeral.MarcaProduto,
                CodigoSistema = produtoGeral.CodigoSistema,
                QuantidadeProd = quantidadeProdutoDepositoAtualizados.Sum(l => l.Quantidade),
                QuantidadeProdutoDeposito = quantidadeProdutoDepositoAtualizados,
                ImagemUrlGeral = registro.ImagemUrlGeral,
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now,
                FornecedorGeralId = registro.FornecedorGeralId,
                CategoriaId = registro.CategoriaId,
                Un = produtoGeral.Un
            };

            if (registro.DataHoraCadastro == null)
            {
                produtoAtualizado.DataHoraCadastro = DateTime.Now;
            }

            _produtoGeralRepository?.Update(produtoAtualizado);
            return _produtoGeralRepository?.GetById(produtoGeral.Id);
        }

        public ProdutoGeral Cadastrar(ProdutoGeral produtoGeral, List<QuantidadeProdutosDepositos> quantidadeProdutosDepositos, string matricula)
        {
            if (produtoGeral == null)
                throw new ArgumentNullException(nameof(produtoGeral));

            if (quantidadeProdutosDepositos == null || !quantidadeProdutosDepositos.Any())
                throw new ArgumentException("A lista de lotes não pode estar vazia.");

            // Inicialize a lista de lotes se ainda não estiver inicializada
            if (produtoGeral.QuantidadeProdutoDeposito == null)
                produtoGeral.QuantidadeProdutoDeposito = new List<QuantidadeProdutosDepositos>();

           
            

            // Associar os lotes restantes ao produto
            foreach (var quantidadeProdutoDeposito in quantidadeProdutosDepositos)
            {
                // Verificar se já existe um depósito com o mesmo NomeDeposito
                var quantidadeDepositoProdutoExistente = produtoGeral.QuantidadeProdutoDeposito
                    .FirstOrDefault(l => l.NomeDeposito == quantidadeProdutoDeposito.NomeDeposito);

                if (quantidadeDepositoProdutoExistente == null)
                {
                    // Se não existir, adicionar o lote à lista
                    produtoGeral.QuantidadeProdutoDeposito.Add(quantidadeProdutoDeposito);
                }
                else
                {
                    // Se existir, atualizar a quantidade  existente
                    quantidadeDepositoProdutoExistente.Quantidade = quantidadeProdutoDeposito.Quantidade;
                }
            }

            // Atualiza os campos do ProdutoGeral nos itens de QuantidadeProdutoDepositos, incluindo o CodigoSistema
            foreach (var quantidadeProdutoDeposito in produtoGeral.QuantidadeProdutoDeposito)
            {
                quantidadeProdutoDeposito.UsuarioId = matricula;
                quantidadeProdutoDeposito.NomeProduto = produtoGeral.NomeProduto;
                quantidadeProdutoDeposito.CodigoSistema = produtoGeral.CodigoSistema; // Adiciona o CódigoSistema
            }

            // Calcula a quantidade total dos Produto, excluindo o primeiro lote
            int quantidadeTotalDepositoProduto = produtoGeral.QuantidadeProdutoDeposito.Sum(l => l.Quantidade);

            // Atribui a quantidade total dos lotes à propriedade Quantidade do produto
            produtoGeral.QuantidadeProd = quantidadeTotalDepositoProduto;

            try
            {
                // Cadastre o produto com os lotes
                _produtoGeralRepository.Add(produtoGeral); // Salva o produto no banco de dados
                produtoGeral = _produtoGeralRepository?.GetById(produtoGeral.Id);
                return produtoGeral; // Retorna o produto após ser salvo, caso precise usar mais tarde
            }
            catch (Exception ex)
            {
                // Registre a exceção para fins de diagnóstico
                Console.WriteLine($"Erro ao salvar o produto: {ex}");
                throw; // Re-throw para que a exceção seja tratada no nível superior
            }
        }


        public void ConfirmarVenda(int id, int quantidadeVendida, string matricula)
        {
            var quantidadeProdutosDepositos = _quantidadeProdutosDepositosRepository.ObterPorId(id);

            if (quantidadeProdutosDepositos == null)
            {
                throw new ApplicationException("Deposito não encontrado.");
            }

            var venda = new VendaProdutoGeral
            {
                QuantidadeProdutoID = quantidadeProdutosDepositos.Id,
                QuantidadeVendida = quantidadeVendida,
                DataVenda = DateTime.Now,
                UsuarioId = matricula,
                CodigoSistema = quantidadeProdutosDepositos.CodigoSistema,
                NomeProduto = quantidadeProdutosDepositos.NomeProduto,
               


            };

            _vendaProdutoGeralRepository.Add(venda);

            quantidadeProdutosDepositos.Quantidade -= quantidadeVendida;

            _quantidadeProdutosDepositosRepository.Update(quantidadeProdutosDepositos);

            var produtoGeral = _produtoGeralRepository.GetById(quantidadeProdutosDepositos.ProdutoGeralId);
            if (produtoGeral == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            produtoGeral.QuantidadeProd = _produtoGeralRepository.GetQuantidadeProdutosDepositosProdutoId
                (produtoGeral.Id)
                .Where(l => l.Quantidade >= 0)
                .Sum(l => l.Quantidade);

            _produtoGeralRepository.Update(produtoGeral);
        }

        public List<ProdutoGeral> Consultar()
        {
            var produtoGeral = _produtoGeralRepository?.GetAll();

            if (produtoGeral == null)
                return new List<ProdutoGeral>();

            // Carregar os lotes para cada produto, começando do segundo lote
            foreach (var produtosGeral in produtoGeral)
            {
                produtosGeral.QuantidadeProdutoDeposito = _produtoGeralRepository?.GetQuantidadeProdutosDepositosProdutoId(produtosGeral.Id);

               
                produtosGeral.QuantidadeProd = produtosGeral.QuantidadeProdutoDeposito.Sum(l => l.Quantidade);
            }

            return produtoGeral;
        }

        public List<QuantidadeProdutosDepositos> ConsultarQuantidadeProdutoDeposito(int produtoGeralId)
        {
            var quantidadeProdutoDeposito = _produtoGeralRepository.GetQuantidadeProdutosDepositosProdutoId(produtoGeralId);
            return quantidadeProdutoDeposito;
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
    }
}
