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
    public class OcorrenciaRepository : BaseRepository<Ocorrencia, int>, IOcorrenciaRepository
    {
        protected readonly DataContext dataContext;


        public override List<Ocorrencia> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Ocorrencia>()
                    .Include(p => p.TipoOcorrencia) //JOIN
                    .Include(p => p.FornecedorGeral)
                    .Include(p => p.Loja)
                    .OrderBy(p => p.Produto)

                    .ToList();
            }
        }
        public  List<Ocorrencia> GetAll(bool ativo)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Ocorrencia>()
                    .Include(p => p.TipoOcorrencia) //JOIN
                    .Include(p => p.FornecedorGeral)
                    .Include(p => p.Loja)
                    .OrderBy(p => p.Produto)

                    .ToList();
            }
        }


        public override Ocorrencia GetById(int id)
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Ocorrencia>()
                    .Include(p => p.TipoOcorrencia) //JOIN
                    .Include(p => p.FornecedorGeral)
                    .Include(p => p.Loja)
                    .FirstOrDefault(p => p.Id == id);
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
