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
    public class QuantidadeProdutosDepositosRepository : BaseRepository<QuantidadeProdutosDepositos, int>, IQuantidadeProdutosDepositosRepository
    {
        public QuantidadeProdutosDepositos ObterPorId(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<QuantidadeProdutosDepositos>().Find(id);
            }
        }

        public List<QuantidadeProdutosDepositos> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                   .Set<QuantidadeProdutosDepositos>()

                   .OrderBy(p => p.Id)
                   .ToList();

            }
        }
    }
}
