using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Venda
    {
        public int VendaID { get; set; }
        public int LoteId { get; set; }
        public string Codigo { get; set; }
        public int NumeroLote {  get; set; }
        public string Matricula { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }

        public Lote Lote { get; set; }
    }
}
