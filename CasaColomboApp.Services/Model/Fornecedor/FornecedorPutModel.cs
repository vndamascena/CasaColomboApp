using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Fornecedor
{
    public class FornecedorPutModel
    {
        [Required(ErrorMessage = "Por favor, informe o id do fornecedor.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do fornecedor.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ da Fornecedor.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Cnpj { get; set; }
    }
}
