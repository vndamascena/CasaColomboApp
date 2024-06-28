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
    public class LojaRepository: BaseRepository<Loja, int>, ILojaRepository
    {
        public override List<Loja> GetAll() 
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Loja>().OrderBy(f => f.Nome).ToList();
            }
        }
        public Loja? GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Loja>()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            }
        }
    }
}
