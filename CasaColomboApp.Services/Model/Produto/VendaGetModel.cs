namespace CasaColomboApp.Services.Model.Produto
{
    public class VendaGetModel
    {
        public int VendaId { get; set; }
        public int LoteID { get; set; }
        public int UsuarioId { get; set; }

        public string Codigo { get; set; }
        public int Quantidade { get; set; }

        public DateTime DataVenda { get; set; }
        public string NumeroLote { get; set; }

    }
}
