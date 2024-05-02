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

        public Produto Cadastrar(Produto produto, List<Lote> lotes)
        {
            if (produto == null)
                throw new ArgumentNullException(nameof(produto));

            if (lotes == null || !lotes.Any())
                throw new ArgumentException("A lista de lotes não pode estar vazia.");

            // Inicialize a lista de lotes se ainda não estiver inicializada
            if (produto.Lote == null)
                produto.Lote = new List<Lote>();

            // Calcula a quantidade total dos lotes
            int quantidadeTotalLotes = lotes.Sum(l => l.QuantidadeLote);

            // Atribui a quantidade total dos lotes à propriedade Quantidade do produto
            produto.Quantidade = quantidadeTotalLotes;

            // Associar os lotes ao produto
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

            try
            {
                // Cadastre o produto com os lotes
                _produtoRepository.Add(produto); // Salva o produto no banco de dados
                return produto; // Retorna o produto após ser salvo, caso precise usar mais tarde
            }
            catch (Exception ex)
            {
                // Registre a exceção para fins de diagnóstico
                Console.WriteLine($"Erro ao salvar o produto: {ex}");
                throw; // Re-throw para que a exceção seja tratada no nível superior
            }
        }

        public Produto Atualizar(Produto produto)
        {
            var registro = ObterPorId(produto.Id);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para edição.");

            // Remove os lotes excluídos do produto
            produto.Lote.RemoveAll(l => l.QuantidadeLote == 0 && (l.NumeroLote == 0));

            // Constrói uma nova lista de lotes para o produto atualizado
            var lotesAtualizados = new List<Lote>();
            foreach (var lote in produto.Lote)
            {
                // Se o lote tiver um ID válido, mantenha o ID ao construir a lista de lotes atualizados
                if (lote.Id != Guid.Empty)
                {
                    lotesAtualizados.Add(new Lote
                    {
                        Id = lote.Id, // Mantém o ID do lote original
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala
                    });
                }
                else
                {
                    // Se o lote não tiver um ID válido, cria um novo lote sem ID
                    lotesAtualizados.Add(new Lote
                    {
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala
                        

                        
                    });
                }
            }

            var produtoAtualizado = new Produto
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
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
                CategoriaId = produto.CategoriaId,
                FornecedorId = produto.FornecedorId,
                Ativo = registro.Ativo,
                ImagemUrl = produto.ImagemUrl,
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now,
                DepositoId = produto.DepositoId


            };
            if (registro.DataHoraCadastro == null)
            {
                produtoAtualizado.DataHoraCadastro = DateTime.Now;
            }

            _produtoRepository?.Update(produtoAtualizado);
            return _produtoRepository?.GetById(produto.Id);
        }







        public Produto Inativar(Guid id)
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

            // Carregar os lotes para cada produto
            foreach (var produto in produtos)
            {
                produto.Lote = _produtoRepository?.GetLotesByProdutoId(produto.Id);
                produto.Quantidade = produto.Lote.Sum(l => l.QuantidadeLote);
            }

            return produtos.Where(p => p.Ativo).ToList();
        }



        public Produto ObterPorId(Guid id)
        {
            var produto = _produtoRepository?.GetById(id);





            return produto;
        }

        public List<Lote> ConsultarLote(Guid produtoId)
        {
            var lotes = _produtoRepository.GetLotesByProdutoId(produtoId);
            return lotes;
        }

        public void ExcluirLote(Guid produtoId, Guid loteId)
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

            // Remove o lote do banco de dados
            _loteRepository?.Remover(loteId);
        }

        public void ConfirmarVenda(Guid loteId, int quantidadeVendida)
        {
            // Obter o lote pelo ID
            var lote = _loteRepository.ObterPorId(loteId);

            if (lote == null)
            {
                throw new ApplicationException("Lote não encontrado.");
            }

            // Verificar se a quantidade vendida é válida
            if (quantidadeVendida <= 0)
            {
                throw new ArgumentException("A quantidade vendida deve ser maior que zero.");
            }

            // Verificar se a quantidade vendida é maior que a quantidade disponível em estoque
            if (quantidadeVendida > lote.QuantidadeLote)
            {
                throw new ApplicationException("Quantidade insuficiente em estoque para a venda.");
            }

            // Atualizar a quantidade do produto no banco de dados
            var produto = _produtoRepository.GetById(lote.ProdutoId); // Obter o produto associado ao lote
            produto.Quantidade -= quantidadeVendida; // Subtrair a quantidade vendida do estoque do produto
            _produtoRepository.Update(produto); // Atualizar o produto no banco de dados

            // Subtrair a quantidade vendida do estoque do lote
            lote.QuantidadeLote -= quantidadeVendida;

            // Criar e salvar a venda
            var venda = new Venda
            {
                LoteId = lote.Id,
                Quantidade = quantidadeVendida,
                DataVenda = DateTime.Now
            };

            _vendaRepository.Add(venda);

            // Atualizar o lote no banco de dados
            _loteRepository.Update(lote);
        }





    }
}
