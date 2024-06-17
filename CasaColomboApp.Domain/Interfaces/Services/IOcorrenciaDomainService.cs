using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IOcorrenciaDomainService
    {
        Ocorrencia Cadastrar(Ocorrencia ocorrencia, string matricula);

       // Ocorrencia Atualizar(Ocorrencia ocorrencia);
     

        List<Ocorrencia> Consultar();

        void BaixaOcorrencia(int id, string matricula);

        Ocorrencia ObterPorId(int id);
        
      
    }
}
