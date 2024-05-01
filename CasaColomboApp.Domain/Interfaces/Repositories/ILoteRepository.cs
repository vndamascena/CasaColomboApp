using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface ILoteRepository : IBaseRepository<Lote, Guid>
    {
        List<Lote> GetAll(bool ativo);

        Lote ObterPorId(Guid id);

        // Adicionar método para remover um lote pelo ID
        void Remover(Guid id);

    }
}
