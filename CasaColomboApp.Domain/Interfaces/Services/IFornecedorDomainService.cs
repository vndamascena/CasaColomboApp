using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IFornecedorDomainService
    {
        Fornecedor Cadastrar(Fornecedor fornecedor);
        Fornecedor Atualizar(Fornecedor fornecedor);
        Fornecedor Delete(Guid id);
        List<Fornecedor> Consultar();
        Fornecedor ObterPorId(Guid id);
    }
}
