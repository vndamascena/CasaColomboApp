namespace CasaColomboApp.Services.Model.Fornecedor
{
    public class FornecedorGetModel
    {
        public int? Id { get; set; }

        public string? Nome { get; set; }

        public string? Cnpj { get; set; }

        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
