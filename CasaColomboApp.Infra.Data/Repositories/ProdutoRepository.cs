using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Infra.Data.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto, Guid>, IProdutoRepository
    {
        protected readonly DataContext _dataContext;



        public override List<Produto> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>()
                    .Include(p => p.Categoria) //JOIN
                    .Include(p => p.Fornecedor) //JOIN

                    .Include(p => p.Deposito)
                    .OrderBy(p => p.Nome)

                    .ToList();
            }
        }
        public List<Produto> GetAll(bool ativo)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                   .Set<Produto>()
                   .Include(p => p.Categoria) //JOIN
                   .Include(p => p.Fornecedor) //JOIN
                   .Include(p => p.Deposito)
                   .OrderBy(p => p.Nome)
                   .ToList();

            }
        }



        public override Produto GetById(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Produto>()
                    .Include(p => p.Categoria) //JOIN
                    .Include(p => p.Fornecedor) //JOIN
                    .Include(p => p.Deposito)
                    .Include(p => p.Lote)
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public List<Lote> GetLotesByProdutoId(Guid produtoId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Lote>()
                    .Where(l => l.ProdutoId == produtoId)
                    .ToList();
            }
        }
    }
}
