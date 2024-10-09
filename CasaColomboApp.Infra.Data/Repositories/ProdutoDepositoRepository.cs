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
    public class ProdutoDepositoRepository : BaseRepository<ProdutoDeposito, int>, IProdutoDepositoRepository
    {
        protected readonly DataContext _dataContext;

        public List<ProdutoDeposito> GetByProdutoGeralId(int produtoGeralId)
        {
            using (var context = new DataContext()) // Substitua 'YourDbContext' pela sua classe de contexto real
            {
                return context.Set<ProdutoDeposito>()
                    .Where(pd => pd.ProdutoGeralId == produtoGeralId)
                    .ToList();
            }
        }

        public List<ProdutoDeposito> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                   .Set<ProdutoDeposito>()

                   .OrderBy(p => p.Id)
                   .ToList();

            }
        }

        public ProdutoDeposito ObterPorId(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<ProdutoDeposito>()
                          .Include(p => p.ProdutoGeral) // Inclui a navegação de ProdutoGeral
                          .FirstOrDefault(p => p.Id == id);
            }
        }

        public ProdutoDeposito ObterPorCodigoSistema(string codigoSistema)
        {
            using (var dataContext = new DataContext())
            {
                // Busque o produto pelo campo CodigoSistema
                return dataContext.Set<ProdutoDeposito>()
                                  .FirstOrDefault(p => p.CodigoSistema == codigoSistema);
            }
        }

        public ProdutoDeposito ObterPorCodigoSistemaEDeposito(string codigoSistema, string deposito)
        {
            using (var dataContext = new DataContext())
            {
                // Busque o produto pelo código do sistema e valide também o depósito
                return dataContext.Set<ProdutoDeposito>()
                                  .Include(p => p.ProdutoGeral) // Inclui a navegação de ProdutoGeral se necessário
                                  .FirstOrDefault(p => p.CodigoSistema == codigoSistema && p.NomeDeposito == deposito);
            }
        }

    }
}
