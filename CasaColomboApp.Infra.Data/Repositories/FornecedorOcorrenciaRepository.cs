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
    public class FornecedorOcorrenciaRepository : BaseRepository<FornecedorOcorrencia, int>, IFornecedorOcorrenciaRepository
    {
        public override List<FornecedorOcorrencia> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<FornecedorOcorrencia>().OrderBy(f => f.Nome).ToList();
            }

            
        }

        public FornecedorOcorrencia? GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<FornecedorOcorrencia>()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            }
        }
    }
}
