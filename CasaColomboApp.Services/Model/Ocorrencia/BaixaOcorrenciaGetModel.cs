using CasaColomboApp.Services.Model.TipoOcorrencia;

namespace CasaColomboApp.Services.Model.Ocorrencia
{
    public class BaixaOcorrenciaGetModel
    {
        public int BaixaId { get; set; }
        public int CodProduto { get; set; }
        public string Produto { get; set; }
        public string Fornecedo { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataTime { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        public TipoOcorrenciaGetModel? TipoOcorrencia { get; set; }

       

    }
}
