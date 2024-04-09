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

        public string Nome { get; set; }

        #region Relacionamento

        public List<Produto>? Produtos { get; set; }

        #endregion




    }
}
