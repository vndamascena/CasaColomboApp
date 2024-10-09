using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Repositories
{
    public interface IProdutoGeralRepository : IBaseRepository<ProdutoGeral, int>
    {
        List<ProdutoGeral> GetAll();

       
        List<ProdutoDeposito> GetProdutosDepositosProdutoId(int produtoGeralId);

    }
}
