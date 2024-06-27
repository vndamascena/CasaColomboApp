using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public  interface IFornecedorOcorrenciaDomainService
    {

        FornecedorOcorrencia Cadastrar(FornecedorOcorrencia fornecedorOcorrencia);
        FornecedorOcorrencia Atualizar(FornecedorOcorrencia fornecedorOcorrencia);
        FornecedorOcorrencia Delete(int id);
        List<FornecedorOcorrencia> Consultar();
        FornecedorOcorrencia ObterPorId(int id);
    }
}
