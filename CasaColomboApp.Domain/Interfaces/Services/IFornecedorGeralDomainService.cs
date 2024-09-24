using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public  interface IFornecedorGeralDomainService
    {

        FornecedorGeral Cadastrar(FornecedorGeral fornecedorGeral);
        FornecedorGeral Atualizar(FornecedorGeral fornecedorGeral);
        FornecedorGeral Delete(int id);
        List<FornecedorGeral> Consultar();
        FornecedorGeral ObterPorId(int id);
    }
}
