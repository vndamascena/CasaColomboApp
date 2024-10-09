using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class ProdutoGeralPutModel
    {
        [Required(ErrorMessage = "Por favor, informe o id do produto.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(3, ErrorMessage = "Informe no mínimo {1} caracteres.")]
        [MaxLength(255, ErrorMessage = "Informe no máximo {1} caracteres.")]
        public string? NomeProduto { get; set; }

        public string? MarcaProduto { get; set; }
        public string? Un { get; set; }

        [Required(ErrorMessage = "Por favor, informe o código do produto.")]
        public string? CodigoSistema { get; set; }

        public string? ImagemUrlGeral { get; set; }

        // Lista de depósitos e suas quantidades para atualização
        public List<ProdutoDepositoPutModel>? Depositos { get; set; }
    }

    public class ProdutoDepositoPutModel
    {
        [Required(ErrorMessage = "Por favor, informe o ID do depósito.")]
        public int DepositoId { get; set; }

        [Required(ErrorMessage = "Por favor, informe a quantidade.")]
        public int Quantidade { get; set; }
    }
}
