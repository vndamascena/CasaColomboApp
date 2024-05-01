namespace CasaColomboApp.Services.Model.Produto
{
    public class VendaGetModel
    {
        public Guid VendaId { get; set; }
        public Guid LoteID { get; set; }
        public int UsuarioId { get; set; }

        public int Quantidade { get; set; }

        public DateTime DataVenda { get; set; }
        public int NumeroLote { get; set; }

    }
}
