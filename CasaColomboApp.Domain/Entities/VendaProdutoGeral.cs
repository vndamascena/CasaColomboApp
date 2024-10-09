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
       
        public string? UsuarioId { get; set; }
        public string? Marca { get; set; }
        public string? CodigoSistema { get; set; }
        public string? NomeProduto { get; set; }
        public int? QuantidadeVendida { get; set; }
        public string? DataVenda { get; set; }
        public DateTime? UploadRelatorioVenda { get; set; }

        public DateTime? DataVendaManual { get; set; }
        public string? NomeDeposito { get; set; }
        public int ProdutoDepositoId { get; set; } // Essa é a FK que vai apontar para ProdutoDeposito
        public ProdutoDeposito? ProdutoDeposito { get; set; } // Propriedade de navegação

    }
}
