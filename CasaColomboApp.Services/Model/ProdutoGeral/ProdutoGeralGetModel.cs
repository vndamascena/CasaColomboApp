using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Depositos;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorGeral;

namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class ProdutoGeralGetModel
    {
        public int? Id { get; set; }
        public string? NomeProduto { get; set; }
        public string? MarcaProduto { get; set; }
        public int? QuantidadeProd { get; set; }
        public string? Un { get; set; }
        public string? CodigoSistema { get; set; }
        public string? ImagemUrlGeral { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        public FornecedorGeralGetModel? Fornecedor { get; set; }
        public CategoriaGetModel? Categoria { get; set; }

        // Adiciona uma lista de depósitos com suas quantidades
        public List<ProdutoDepositoGetModel>? ProdutoDeposito { get; set; }
    }

    public class ProdutoDepositoGetModel
    {
        public int DepositoId { get; set; }
        public string? NomeDeposito { get; set; } // Para incluir o nome do depósito
        public int Quantidade { get; set; }
    }
}
