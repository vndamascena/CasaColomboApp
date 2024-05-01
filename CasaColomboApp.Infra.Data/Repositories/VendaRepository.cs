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
    public class VendaRepository : BaseRepository<Venda, Guid>, IVendaRepository
    {
        public List<Venda> GetVendasByLoteId(Guid loteId)
        {
            using(var dataContext = new DataContext())
            {
                return dataContext.Set<Venda>()
                           .Include(v => v.Lote) // Inclui o lote relacionado à venda
                           .Where(v => v.LoteId == loteId)
                           .ToList(); ;

            }
        }

        public List<Venda> GetVendasByUsuarioId(int usuarioId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Venda>().Where(v => v.UsuarioId == usuarioId).ToList();
            }
        }
    }

}
