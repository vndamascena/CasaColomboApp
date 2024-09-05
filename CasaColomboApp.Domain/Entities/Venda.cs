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
        public string? Nomeproduto { get; set; }
        public string NumeroLote {  get; set; }
        public string UsuarioId { get; set; }
        public string? Marca { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataVenda { get; set; }

        public Lote Lote { get; set; }
    }
}
