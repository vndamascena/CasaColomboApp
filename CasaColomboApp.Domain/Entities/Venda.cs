using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Venda
    {
        public Guid VendaID { get; set; }
        public Guid LoteId { get; set; }
        public int NumeroLote {  get; set; }
        public int UsuarioId { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }

        public Lote Lote { get; set; }
    }
}
