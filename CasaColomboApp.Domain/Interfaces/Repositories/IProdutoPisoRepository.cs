using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IProdutoPisoRepository : IBaseRepository<ProdutoPiso,int>
    {
        List<ProdutoPiso> GetAll(bool ativo);
        List<Lote> GetLotesByProdutoId( int produtoPisoId);

    }
}
