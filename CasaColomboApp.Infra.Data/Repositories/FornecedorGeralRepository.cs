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
    public class FornecedorGeralRepository : BaseRepository<FornecedorGeral, int>, IFornecedorGeralRepository
    {
        public override List<FornecedorGeral> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<FornecedorGeral>().OrderBy(f => f.Nome).ToList();
            }

            
        }

        public FornecedorGeral? GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<FornecedorGeral>()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            }
        }
    }
}
