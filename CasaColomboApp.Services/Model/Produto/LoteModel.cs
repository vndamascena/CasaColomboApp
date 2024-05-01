using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto
{
    public class LoteModel
    {



        [Required(ErrorMessage = "Informe o número do lote.")]
        public int NumeroLote { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do lote.")]
        public int QuantidadeLote { get; set; }

        
        public string Ala {  get; set; }
        // Outras propriedades do lote...





    }
}
