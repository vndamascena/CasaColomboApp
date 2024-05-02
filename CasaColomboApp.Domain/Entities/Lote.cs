using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Lote
    {
        public int Id { get; set; }

        public int ProdutoId { get; set; } // Referência ao ID do produto ao qual o lote pertence
        public int NumeroLote { get; set; } // Identificador único do lote
        public int QuantidadeLote { get; set; }
       
        public string Ala {  get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }

        // Relacionamento com o produto
        public Produto Produto { get; set; }
        public List<Venda> Vendas { get; set; }

        public Lote()
        {
            DataUltimaAlteracao = DateTime.Now; // Define a data e hora atual como padrão
            Vendas = new List<Venda>();
        }
    }
}
