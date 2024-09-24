using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto.Piso
{
    public class LoteModel
    {

        
        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(5, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Codigo { get; set; }
        
        
        public string? NumeroLote { get; set; }

       
        public int QuantidadeLote { get; set; }

        public int QtdEntrada { get; private set; }

        public string Ala {  get; set; }
        // Outras propriedades do lote...

        public LoteModel(int quantidadeLote)
        {
            QuantidadeLote = quantidadeLote;
            QtdEntrada = quantidadeLote;
        }



    }
}
