﻿using CasaColomboApp.Domain.Entities;
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
            produto.Lote.RemoveAll(l => l.QuantidadeLote == 0 && string.IsNullOrEmpty(l.NumeroLote));

            // Constrói uma nova lista de lotes para o produto atualizado
            var lotesAtualizados = new List<Lote>();
            foreach (var lote in produto.Lote)
            {
                // Se o lote tiver um ID válido, mantenha o ID ao construir a lista de lotes atualizados
                if (lote.Id != 0)
                {
                    lotesAtualizados.Add(new Lote
                    {
                        Id = lote.Id, // Mantém o ID do lote original
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala,
                        Codigo = lote.Codigo
                    });
                }
                else
                {
                    // Se o lote não tiver um ID válido, cria um novo lote sem ID
                    lotesAtualizados.Add(new Lote
                    {
                        NumeroLote = lote.NumeroLote,
                        QuantidadeLote = lote.QuantidadeLote,
                        Ala = lote.Ala,
                        Codigo = lote.Codigo



                    });
                }
            }

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

            // Carregar os lotes para cada produto
            foreach (var produto in produtos)
            {
                produto.Lote = _produtoRepository?.GetLotesByProdutoId(produto.Id);
                produto.Quantidade = produto.Lote.Sum(l => l.QuantidadeLote);
            }

            return produtos.Where(p => p.Ativo).ToList();
        }



        public Produto ObterPorId(int id)
        {
            var produto = _produtoRepository?.GetById(id);





            return produto;
        }

        public List<Lote> ConsultarLote(int produtoId)
        {
            var lotes = _produtoRepository.GetLotesByProdutoId(produtoId);
            return lotes;
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

            // Remove o lote do banco de dados
            _loteRepository?.Remover(loteId);
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
                Matricula = matricula,
                Codigo = lote.Codigo
            };

            _vendaRepository.Add(venda);

            // Verificar se há quantidade suficiente em estoque
            if (quantidadeVendida <= lote.QuantidadeLote)
            {
                // Atualizar a quantidade vendida do lote
                lote.QuantidadeLote -= quantidadeVendida;
            }
            else
            {
                // Se não houver estoque suficiente, vendemos o que temos e registramos o restante como negativo
                quantidadeVendida = lote.QuantidadeLote;
                lote.QuantidadeLote = 0; // Nenhum estoque restante
            }

            // Atualizar o lote no banco de dados
            _loteRepository.Update(lote);
        }





    }
}
