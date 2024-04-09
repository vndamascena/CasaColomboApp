using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto
{
    public class ProdutoPutModel
    {
        [Required(ErrorMessage = "Por favor, informe o id do produto.")]
        public Guid? Id { get; set; }


        [Required(ErrorMessage = "Informe o codigo do produto.")]
        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Codigo { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(100, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }

        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Infome no maximo {1} caracteres")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "Informe a quantidade de produto.")]
        [MinLength(0, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Infome no maximo {1} caracteres")]
        public string? Quantidade { get; set; }

        [MinLength(2, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(20, ErrorMessage = "Infome no maximo {1} caracteres")]
        public string? Lote { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(500, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição do produto.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informeo ID da categoria para o produto.")]
        public Guid? CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do fornecedor para o produto.")]
        public Guid? FornecedorId { get; set; }

        public int? DepositoId { get; set; }
    }
}
