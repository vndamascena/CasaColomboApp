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
    public class DepositoRepository : BaseRepository<Deposito, int>, IDepositoRepository
    {
        public override List<Deposito> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Deposito>().OrderBy(c => c.NomeDeposito).ToList();
            }
        }

    }
}
