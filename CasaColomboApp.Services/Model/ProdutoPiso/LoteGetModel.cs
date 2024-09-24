namespace CasaColomboApp.Services.Model.Produto.Piso
{
    public class LoteGetModel
    {
        public string? Codigo { get; set; }
        public string? UsuarioId { get; set; }
        public string? NumeroLote { get; set; }
        public int? QuantidadeLote { get; set; }
        public string? NomeProduto { get; set; }
        public string? Ala {  get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public int? QtdEntrada  { get; set; }
        public int Id { get; set; }
        public string? Marca { get; set; }

    }
}