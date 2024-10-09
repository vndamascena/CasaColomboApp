using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class ProdutoGeralPostModel
    {
        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(255, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string? NomeProduto { get; set; }

        public string? MarcaProduto { get; set; }

        public string? Un { get; set; }

        [Required(ErrorMessage = "Por favor, informe o código do produto.")]
        public string? CodigoSistema { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID da categoria para o produto.")]
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do fornecedor para o produto.")]
        public int? FornecedorGeralId { get; set; }

        public List<ProdutoDepositoModel> Depositos { get; set; } = new List<ProdutoDepositoModel>();

        public string? ImagemUrlGeral { get; set; }
    }

    public class ProdutoDepositoModel
    {
        [Required(ErrorMessage = "Por favor, informe o ID do depósito.")]
        public int DepositoId { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade.")]
        public int Quantidade { get; set; }
    }


}
