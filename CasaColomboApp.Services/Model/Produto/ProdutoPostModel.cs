using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto
{
    public class ProdutoPostModel
    {
        [Required(ErrorMessage = "Informe o codigo do produto.")]
        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(5, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Codigo { get; set; }


        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(15, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }


        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(15, ErrorMessage = "Infome no maximo {1} caracteres")]
        public string? Marca { get; set; }

        

        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(2, ErrorMessage = "Infome no maximo {1} caracteres")]
        public string? Pei { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(70, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe a descrição do produto.")]
        public string? Descricao { get; set; }

        public int? PecasCaixa { get; set; }

        public string? MetroQCaixa { get; set; }

        
        public decimal? PrecoCaixa { get; set; }

       
        public decimal? PrecoMetroQ { get; set; }

        [Required(ErrorMessage = "Por favor, informeo ID da categoria para o produto.")]
        public int? CategoriaId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do fornecedor para o produto.")]
        public int? FornecedorId { get; set; }

        [Required(ErrorMessage = "Por favor, informe o ID do deposito para o produto.")]
        public int? DepositoId { get; set; }

        // Novo campo para a URL da imagem

        public string? ImagemUrl { get; set; }

        public List<LoteModel>? Lote { get; set; }






    }
}
