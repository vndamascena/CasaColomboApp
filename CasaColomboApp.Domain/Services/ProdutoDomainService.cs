using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace CasaColomboApp.Domain.Services
{
    public class ProdutoDomainService : IProdutoDomainService
    {
        private readonly IProdutoRepository? _produtoRepository;
        private readonly ILoteRepository? _loteRepository;
        private readonly IVendaRepository? _vendaRepository;

        public ProdutoDomainService(IProdutoRepository? produtoRepository, ILoteRepository? loteRepository, IVendaRepository? vendaRepository)
        {
            _produtoRepository = produtoRepository;
            _loteRepository = loteRepository;
            _vendaRepository = vendaRepository;
        }

        public Produto Cadastrar(Produto produto, List<Lote> lotes, string matricula)
        {

            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (lotes == null || !lotes.Any())
                throw new ArgumentException("A lista de lotes não pode estar vazia.");

            // Inicialize a lista de lotes se ainda não estiver inicializada
            if (produto.Lote == null)
                produto.Lote = new List<Lote>();

            // Se o produto ainda não tiver nenhum lote, considere o primeiro lote como o principal
            if (!produto.Lote.Any())
            {
                produto.Lote.Add(lotes.First()); // Adicione o primeiro lote à lista de lotes do produto
                lotes.RemoveAt(0); // Remova o primeiro lote da lista de lotes
            }

            // Associar os lotes restantes ao produto
            foreach (var lote in lotes)
            {
                // Verificar se já existe um lote com o mesmo númeroLote
                var loteExistente = produto.Lote.FirstOrDefault(l => l.NumeroLote == lote.NumeroLote);

                if (loteExistente == null)
                {
                    // Se não existir, adicionar o lote à lista
                    produto.Lote.Add(lote);
                }
                else
                {
                    // Se existir, atualizar a quantidade do lote existente
                    loteExistente.QuantidadeLote = lote.QuantidadeLote;
                }
            }
            foreach (var lote in produto.Lote)
            {
                lote.UsuarioId = matricula;
                lote.NomeProduto = produto.Nome;
                lote.Marca = produto.Marca;

            }
            // Calcula a quantidade total dos lotes, excluindo o primeiro lote
            int quantidadeTotalLotes = produto.Lote.Skip(1).Sum(l => l.QuantidadeLote);

            // Atribui a quantidade total dos lotes à propriedade Quantidade do produto
            produto.Quantidade = quantidadeTotalLotes;

            try
            {
                // Cadastre o produto com os lotes
                _produtoRepository.Add(produto); // Salva o produto no banco de dados
                produto = _produtoRepository?.GetById(produto.Id);
                return produto; // Retorna o produto após ser salvo, caso precise usar mais tarde
            }
            catch (Exception ex)
            {
                // Registre a exceção para fins de diagnóstico
                Console.WriteLine($"Erro ao salvar o produto: {ex}");
                throw; // Re-throw para que a exceção seja tratada no nível superior
            }
        }


        public Produto Atualizar(Produto produto, string matricula)
        {
            var registro = ObterPorId(produto.Id);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para edição.");

            // Remove os lotes excluídos do produto
            produto.Lote.RemoveAll(l => l.QuantidadeLote == 0 && string.IsNullOrEmpty(l.NumeroLote));

            // Constrói uma nova lista de lotes para o produto atualizado
            var lotesAtualizados = new List<Lote>();
            foreach (var lote in produto.Lote)
            {
                var loteExistente = _loteRepository.ObterPorId(lote.Id);

                if (loteExistente != null)
                {
                    // Se os campos QtdEntrada e NomeProduto não forem fornecidos, mantenha os valores atuais
                    loteExistente.NumeroLote = lote.NumeroLote;
                    loteExistente.QuantidadeLote = lote.QuantidadeLote;
                    loteExistente.Ala = lote.Ala;
                    loteExistente.Codigo = lote.Codigo;
                    loteExistente.NomeProduto = string.IsNullOrEmpty(lote.NomeProduto) ? loteExistente.NomeProduto : lote.NomeProduto;
                    loteExistente.QtdEntrada = lote.QtdEntrada == 0 ? loteExistente.QtdEntrada : lote.QtdEntrada;
                    loteExistente.DataUltimaAlteracao = DateTime.Now;
                    loteExistente.UsuarioId = matricula;
                    loteExistente.Marca = string.IsNullOrEmpty(lote.Marca) ? loteExistente.Marca : lote.Marca;

                    lotesAtualizados.Add(loteExistente);
                }
                else
                {
                    // Adiciona um novo lote com status Ativo
                    lotesAtualizados.Add(new Lote
                    {
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala,
                        Codigo = lote.Codigo,
                        NomeProduto = lote.NomeProduto,
                        Ativo = true, // Define o novo lote como ativo
                        DataUltimaAlteracao = DateTime.Now,
                        DataEntrada = DateTime.Now,
                        QtdEntrada = lote.QtdEntrada,
                        UsuarioId = matricula,
                        Marca = lote.Marca,
                    });
                }
            }

            // Identifica lotes que foram removidos do produto e devem ser desativados

            var produtoAtualizado = new Produto
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Marca = produto.Marca,
                Pei = produto.Pei,
                Quantidade = lotesAtualizados.Sum(l => l.QuantidadeLote),
                Lote = lotesAtualizados,
                Descricao = produto.Descricao,
                PrecoCaixa = produto.PrecoCaixa,
                MetroQCaixa = produto.MetroQCaixa,
                PrecoMetroQ = produto.PrecoMetroQ,
                PecasCaixa = produto.PecasCaixa,
                Ativo = registro.Ativo,
                ImagemUrl = registro.ImagemUrl,
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now,
                DepositoId = registro.DepositoId,
                FornecedorId = registro.FornecedorId,
                CategoriaId = registro.CategoriaId,
            };

            if (registro.DataHoraCadastro == null)
            {
                produtoAtualizado.DataHoraCadastro = DateTime.Now;
            }

            _produtoRepository?.Update(produtoAtualizado);
            return _produtoRepository?.GetById(produto.Id);
        }








        public Produto Inativar(int id)
        {

            var produto = ObterPorId(id);

            if (produto == null)
                throw new ApplicationException("Produto não encontrado para exclusão.");

            produto.Ativo = false;
            produto.DataHoraAlteracao = DateTime.Now;

            _produtoRepository?.Update(produto);

            return produto;
        }

        public List<Produto> Consultar()
        {
            var produtos = _produtoRepository?.GetAll(true);

            if (produtos == null)
                return new List<Produto>();

            // Carregar os lotes para cada produto, começando do segundo lote
            foreach (var produto in produtos)
            {
                produto.Lote = _produtoRepository?.GetLotesByProdutoId(produto.Id);

                // Se o produto tiver mais de um lote, exclua o primeiro lote da lista
                if (produto.Lote.Count > 1)
                    produto.Lote.RemoveAt(0);

                produto.Quantidade = produto.Lote.Sum(l => l.QuantidadeLote);
            }

            return produtos.Where(p => p.Ativo).ToList();
        }



        public Produto ObterPorId(int id)
        {
            var produto = _produtoRepository?.GetById(id);

            if (produto != null)
            {
                // Filtra os lotes inativos
                produto.Lote = produto.Lote.Where(l => l.Ativo).ToList();
            }

            return produto;
        }


      
        public List<Lote> ConsultarLote(int produtoId)
        {
            var lotes = _produtoRepository.GetLotesByProdutoId(produtoId);
            return lotes.Where(l => l.Ativo).ToList();
        }

        public void ExcluirLote(int produtoId, int loteId)
        {
            // Verifica se o lote pertence ao produto
            var lote = _loteRepository.ObterPorId(loteId);
            if (lote == null)
            {
                throw new ApplicationException("O lote especificado não foi encontrado.");
            }

            if (lote.ProdutoId != produtoId)
            {
                throw new ApplicationException("O lote não pertence ao produto especificado.");
            }

            // Inativa o lote em vez de removê-lo
            lote.Ativo = false;
            _loteRepository.Update(lote);

        }

        public void ConfirmarVenda(int loteId, int quantidadeVendida, string matricula)
        {
            // Obter o lote pelo ID
            var lote = _loteRepository.ObterPorId(loteId);

            if (lote == null)
            {
                throw new ApplicationException("Lote não encontrado.");
            }

            // Criar e salvar a venda
            var venda = new Venda
            {
                LoteId = lote.Id,
                Quantidade = quantidadeVendida,
                DataVenda = DateTime.Now,
                NumeroLote = lote.NumeroLote,
                UsuarioId  = matricula,
                Codigo = lote.Codigo,
                Nomeproduto = lote.NomeProduto,
                Marca = lote.Marca,
              
                
            };

            _vendaRepository.Add(venda);

            // Atualizar a quantidade vendida do lote, permitindo quantidade negativa
            lote.QuantidadeLote -= quantidadeVendida;

            // Atualizar o lote no banco de dados
            _loteRepository.Update(lote);

            // Obter o produto associado ao lote
            var produto = _produtoRepository.GetById(lote.ProdutoId);

            if (produto == null)
            {
                throw new ApplicationException("Produto não encontrado.");
            }

            // Recalcular a quantidade total do produto, excluindo lotes com quantidade negativa
            produto.Quantidade = _produtoRepository.GetLotesByProdutoId(produto.Id)
                                                    .Where(l => l.QuantidadeLote >= 0)
                                                    .Sum(l => l.QuantidadeLote);

            // Atualizar o produto no banco de dados
            _produtoRepository.Update(produto);
        }






    }
}
