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
    public class TipoOcorrenciaRepository: BaseRepository<TipoOcorrencia, int>, ITipoOcorrenciaRepository
    {
        public override List<TipoOcorrencia> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<TipoOcorrencia>().OrderBy(f => f.Nome).ToList();
            }
        }
        public TipoOcorrencia? GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<TipoOcorrencia>()
                .Where(f => f.Id == id)
                .FirstOrDefault();

            }
        }
    }
}
