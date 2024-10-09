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
    public class VendaProdutoGeralRepository : BaseRepository<VendaProdutoGeral, int>, IVendaProdutoGeralRepository
    {
        public List<VendaProdutoGeral> GetVendaProdutoGeralByUsuarioId(string matricula)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<VendaProdutoGeral>().Where(v => v.UsuarioId == matricula).ToList();
            }
        }

        public List<VendaProdutoGeral> GetVendaProutoGeralByQtdPDId(int produtoDepositoId)
        {

            using (var dataContext = new DataContext())
            {
                return dataContext.Set<VendaProdutoGeral>()
                           .Include(v => v.ProdutoDeposito) // Inclui o lote relacionado à venda
                           .Where(v => v.ProdutoDepositoId == produtoDepositoId)
                           .ToList(); ;

            }
        }
    }
}
