using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Loja
{
    public class LojaPostModel
    {
        [Required(ErrorMessage = "Informe o nome da loja.")]
        [MinLength(2, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(15, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }
    }
}
