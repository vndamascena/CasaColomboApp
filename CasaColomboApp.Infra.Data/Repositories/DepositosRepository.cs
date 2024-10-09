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
    public class DepositosRepository : BaseRepository<Depositos, int>, IDepositosRepository
    {
        public override List<Depositos> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Depositos>().OrderBy(f => f.Nome).ToList();
            }


        }

        public Depositos? GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Depositos>()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            }
        }
    }
}
