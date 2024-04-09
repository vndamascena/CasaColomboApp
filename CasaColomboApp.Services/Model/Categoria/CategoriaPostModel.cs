using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Categoria
{
    public class CategoriaPostModel
    {
        [Required(ErrorMessage = "Informe o nome da categoria.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string Nome { get; set; }

    
    }
}
