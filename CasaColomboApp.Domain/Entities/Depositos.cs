using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Depositos
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        #region Relacionamento
        // Relacionamento Muitos-para-Muitos com a tabela intermediária ProdutoDeposito
        public List<ProdutoDeposito> ProdutoDepositos { get; set; }
        #endregion
    }

}
