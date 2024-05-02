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
    public class CategoriaRepository : BaseRepository<Categoria, int>, ICategoriaRepository
    {
        public CategoriaRepository() // Adicionado um construtor vazio
        {
        }

        public override List<Categoria> GetAll()
        {
            using (var dataContext = new DataContext())
            {
                return dataContext
                    .Set<Categoria>()
                    .OrderBy(c => c.Nome)
                    .ToList();
            }
        }
    }
}
