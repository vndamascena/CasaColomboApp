namespace CasaColomboApp.Services.Model.ProdutoGeral
{
    public class QuantidadeProdutosDepositosGetModel
    {
        public int? Id { get; set; }
        public int? Quantidade { get; set; }
        public string? NomeDeposito { get; set; }
        public string? UsuarioId { get; set; }
        public string? NomeProduto { get; set; }
        public string? CodigoSistema { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
    }
}
