using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface ITipoOcorrenciaDomainService
    {
        TipoOcorrencia Cadastrar(TipoOcorrencia tipoOcorrencia);
        TipoOcorrencia Atualizar(TipoOcorrencia tipoOcorrencia);
        TipoOcorrencia Delete(int id);
        List<TipoOcorrencia> Consultar();
        TipoOcorrencia ObterPorId(int id);

    }
}
