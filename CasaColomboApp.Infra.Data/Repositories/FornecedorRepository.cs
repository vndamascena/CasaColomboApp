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
    public class FornecedorRepository : BaseRepository<Fornecedor, Guid>, IFornecedorRepository
    {
        public override List<Fornecedor> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<Fornecedor>().OrderBy(f => f.Nome).ToList();
            }
        }
    }
}
