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
        public string? UsuarioId { get; set; }
        public int ProdutoPisoId { get; set; } // Referência ao ID do produto ao qual o lote pertence
        public string? Codigo { get; set; }
        public string NumeroLote { get; set; } // Identificador único do lote
        public int QuantidadeLote { get; set; }
        public bool Ativo { get; set; }
        public string? NomeProduto { get; set; }
        public string Ala {  get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public DateTime? DataEntrada { get; set; }
        public string? Marca { get; set; }
        public int QtdEntrada { get;  set; }
        // Relacionamento com o produto
        public ProdutoPiso ProdutoPiso { get; set; }
        public List<Venda> Vendas { get; set; }

        public Lote()
        {
            DataUltimaAlteracao = DateTime.Now; // Define a data e hora atual como padrão
            Vendas = new List<Venda>();
            DataEntrada = DateTime.Now;
            Ativo = true;
           
        }
    }
}
