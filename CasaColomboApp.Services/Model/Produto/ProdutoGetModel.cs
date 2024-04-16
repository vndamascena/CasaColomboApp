using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor;

namespace CasaColomboApp.Services.Model.Produto
{
    public class ProdutoGetModel
    {
        public Guid? Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nome { get; set; }
        public string? Marca { get; set; }
        public string? Quantidade { get; set; }
        public string? Lote { get; set; }
        public string? Descricao { get; set; }

        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }

        public FornecedorGetModel? Fornecedor { get; set; }
        public CategoriaGetModel? Categoria { get; set; }

        public DepositoGetModel? Deposito { get; set; }

        public string? ImagemUrl { get; set; }


    }
}
