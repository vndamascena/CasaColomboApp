using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class VendaProdutoGeral
    {
        public int VendaProdutoGeralId { get; set; }
        public int QuantidadeProdutoID { get; set; }
        public string? UsuarioId { get; set; }
        public string? Marca { get; set; }
        public string? CodigoSistema { get; set; }
        public string? NomeProduto { get; set; }
        public int? QuantidadeVendida { get; set; }

        public DateTime? DataVenda { get; set; }

        public QuantidadeProdutosDepositos QuantidadeProdutosDepositos { get; set; }
    }
}
