using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Infra.Data.Repositories
{
    public class LoteRepository : BaseRepository<Lote, Guid>, ILoteRepository
    {
        public List<Lote> GetAll(bool ativo)
        {
            throw new NotImplementedException();
        }

        public Lote ObterPorId(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Lote>().Find(id);
            }
        }

        // Implementação do método para remover um lote pelo ID
        public void Remover(Guid id)
        {
            using (var dataContext = new DataContext())
            {
                var entity = dataContext.Set<Lote>().Find(id);
                if (entity != null)
                {
                    dataContext.Set<Lote>().Remove(entity);
                    dataContext.SaveChanges();
                }
            }
        }
    }
}
