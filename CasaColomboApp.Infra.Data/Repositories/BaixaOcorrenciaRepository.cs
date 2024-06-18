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
    public class BaixaOcorrenciaRepository: BaseRepository<BaixaOcorrencia , int>, IBaixaOcorrenciaRepository
    {
        protected readonly DataContext dataContext;

        public override List<BaixaOcorrencia> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<BaixaOcorrencia>()
                    .Include(p => p.Ocorrencia) //JOIN

                    .OrderBy(p => p.BaixaId)

                    .ToList();
            }
        }

        public List<BaixaOcorrencia> GetBaixaOcorrenciaId(int ocorrenciaId)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set< BaixaOcorrencia>()
                           .Include(v => v.Ocorrencia) // Inclui o lote relacionado à venda
                           .Where(v => v.TipoOcorrenciaId == ocorrenciaId)
                           .ToList(); ;

            }
        }

        public List<BaixaOcorrencia> GetByUsuarioId(string matricula)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext.Set<BaixaOcorrencia>().Where(v => v.UsuarioId == matricula).ToList();
            }
        }

    }
}
