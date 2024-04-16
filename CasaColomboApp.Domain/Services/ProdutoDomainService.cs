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

        public ProdutoDomainService(IProdutoRepository? produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Produto Cadastrar(Produto produto)
        {
            _produtoRepository?.Add(produto);
            produto = _produtoRepository?.GetById(produto.Id.Value);

            return produto;
        }

        public Produto Atualizar(Produto produto)
        {
            var registro = ObterPorId(produto.Id.Value);

            if (registro == null)
                throw new ApplicationException("Produto não encontrado para edição.");

            var produtoAtualizado = new Produto
            {
                Id = produto.Id,
                Codigo = produto.Codigo,
                Nome = produto.Nome,
                Marca = produto.Marca,
                Quantidade = produto.Quantidade,
                Lote = produto.Lote,
                Descricao = produto.Descricao,
                CategoriaId = produto.CategoriaId,
                FornecedorId = produto.FornecedorId,
                Ativo = registro.Ativo,
                DataHoraCadastro = registro.DataHoraCadastro,
                DataHoraAlteracao = DateTime.Now,
                DepositoId = produto.DepositoId,
                ImagemUrl = produto.ImagemUrl,


            };

           

            _produtoRepository?.Update(produtoAtualizado);
            return _produtoRepository?.GetById(produto.Id.Value);
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

            

            return produtos.Where(p => p.Ativo).ToList();
        }

     

        public Produto ObterPorId(Guid id)
        {
            var produto = _produtoRepository?.GetById(id);

           



            return produto;
        }
    }
}
