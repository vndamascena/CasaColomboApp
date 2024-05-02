namespace CasaColomboApp.Services.Model.Produto
{
    public class VendaGetModel
    {
        public int VendaId { get; set; }
        public int LoteID { get; set; }
        public int UsuarioId { get; set; }

        public int Quantidade { get; set; }

        public DateTime DataVenda { get; set; }
        public int NumeroLote { get; set; }

    }
}
