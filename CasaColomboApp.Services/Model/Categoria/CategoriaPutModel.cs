using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Categoria
{
    public class CategoriaPutModel
    {
        [Required(ErrorMessage = "Por favor, informe o id do produto.")]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }
    }
}
