namespace CasaColomboApp.Services.Model.Produto.Piso
{
    public class VendaPisoGetModel
    {
        public int VendaId { get; set; }
        public int LoteID { get; set; }
        public string? UsuarioId { get; set; }
        public string? Marca { get; set; }
        public string? Codigo { get; set; }
        public string? NomeProduto {  get; set; }
        public int? Quantidade { get; set; }
        
        public DateTime? DataVenda { get; set; }
        public string? NumeroLote { get; set; }

    }
}
