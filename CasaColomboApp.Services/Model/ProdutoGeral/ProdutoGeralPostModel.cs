using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class ProdutoGeralPostModel
    {
       

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(3, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(255, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? NomeProduto { get; set; }

        public string? MarcaProduto { get; set; }

        public string? Un { get; set; }

        [Required(ErrorMessage = "Por favor, informe o codígo do produto.")]
        public string? CodigoSistema { get; set; }

        //public string? Preco { get; set; }

        [Required(ErrorMessage = "Por favor, informeo ID da categoria para o produto.")]
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do fornecedor para o produto.")]
        public int? FornecedorGeralId { get; set; }

        

        public string? ImagemUrlGeral { get; set; }
        public List<QuantidadeProdutosDepositosPostModel> QuantidadeProdutoDeposito { get; set; }
    }
}
