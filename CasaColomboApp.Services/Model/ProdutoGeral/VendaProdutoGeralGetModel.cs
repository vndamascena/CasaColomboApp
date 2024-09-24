namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class VendaProdutoGeralGetModel
    {
        public int VendaProdutoGeralId { get; set; }
        public int QuantidadeProdutoID { get; set; }
        public string? UsuarioId { get; set; }
        public string? Marca { get; set; }
        public string? CodigoSistema { get; set; }
        public string? NomeProduto { get; set; }
        public int? QuantidadeVendida { get; set; }

        public DateTime? DataVenda { get; set; }
       
    }
}
