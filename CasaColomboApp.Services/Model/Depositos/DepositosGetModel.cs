namespace CasaColomboApp.Services.Model.Depositos
{
    public class DepositosGetModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }
    }
}
