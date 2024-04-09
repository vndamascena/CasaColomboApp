namespace CasaColomboApp.Services.Model.Categoria
{
    public class CategoriaGetModel
    {
        public Guid? Id { get; set; }

        public string? Nome { get; set; }

        public DateTime? DataHoraCadastro { get; set; }
        public DateTime? DataHoraAlteracao { get; set; }
    }
}
