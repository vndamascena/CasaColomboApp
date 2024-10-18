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
            using (var context = new DataContext())
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
                return dataContext.Set<ProdutoDeposito>().Find(id);

            }
        }

        public ProdutoDeposito ObterId(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<ProdutoDeposito>()
                                  .Include(pd => pd.ProdutoGeral) // Certifique-se de incluir ProdutoGeral
                                  .FirstOrDefault(pd => pd.Id == id); // Use FirstOrDefault para evitar Find, que não suporta Include
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


        public ProdutoDeposito ObterPorIdProduto(int idDeposito, int idPrododuto)
        {
            using (var dataContext = new DataContext())
            {
                // Busque o produto pelo código do sistema e valide também o depósito
                return dataContext.Set<ProdutoDeposito>()
                                  .Include(p => p.ProdutoGeral) // Inclui a navegação de ProdutoGeral se necessário
                                  .FirstOrDefault(p => p.DepositoId == idDeposito && p.ProdutoGeralId == idPrododuto);
            }
        }

    }
}
