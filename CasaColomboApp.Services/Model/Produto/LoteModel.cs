using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto
{
    public class LoteModel
    {

        [Required(ErrorMessage = "Informe o codigo do produto.")]
        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(5, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Informe o número do lote.")]
        public int NumeroLote { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do lote.")]
        public int QuantidadeLote { get; set; }


        
        public string Ala {  get; set; }
        // Outras propriedades do lote...





    }
}
