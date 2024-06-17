using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IBaixaOcorrenciaRepository : IBaseRepository<BaixaOcorrencia, int>
    {
        List<BaixaOcorrencia> GetBaixaOcorrenciaId(int ocorrenciaId);
        List<BaixaOcorrencia> GetByUsuarioId(string matricula);

    }
}
