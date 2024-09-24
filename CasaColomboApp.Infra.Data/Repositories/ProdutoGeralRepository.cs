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
    public class ProdutoGeralRepository : BaseRepository<ProdutoGeral, int>, IProdutoGeralRepository
    {
        protected readonly DataContext _dataContext;


        public List<ProdutoGeral> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                   .Set<ProdutoGeral>()
                   .Include(p => p.Categoria) //JOIN
                   .Include(p => p.Fornecedor) //JOIN

                   .OrderBy(p => p.NomeProduto)
                   .ToList();

            }
        }

        public override ProdutoGeral GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<ProdutoGeral>()
                    .Include(p => p.Categoria) //JOIN
                    .Include(p => p.Fornecedor) //JOIN

                    .Include(p => p.QuantidadeProdutoDeposito)
                    .FirstOrDefault(p => p.Id == id);
            }
        }

        public List<QuantidadeProdutosDepositos> GetQuantidadeProdutosDepositosProdutoId(int produtoGeralId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<QuantidadeProdutosDepositos>()
                    .Where(l => l.ProdutoGeralId == produtoGeralId)
                    .ToList();
            }
        }

      
    }
}
