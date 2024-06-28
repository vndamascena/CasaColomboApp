using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Loja
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }

        #region Relacionamento

        public List<Ocorrencia>? Ocorrencia { get; set; }
        #endregion
    }
}
