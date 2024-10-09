using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IVendaProdutoGeralRepository : IBaseRepository<VendaProdutoGeral, int>
    {
        List<VendaProdutoGeral> GetVendaProutoGeralByQtdPDId(int produtoDepositoId);
        List<VendaProdutoGeral> GetVendaProdutoGeralByUsuarioId(string matricula);
    }
}
