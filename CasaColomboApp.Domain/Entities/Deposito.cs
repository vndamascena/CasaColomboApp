using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Deposito
    {
        public int DepositoId { get; set; }

        public string NomeDeposito { get; set; }

        #region Relacionamento

        public List<ProdutoPiso>? ProdutosPiso { get; set; }

        #endregion




    }
}
