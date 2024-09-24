using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Services.Model.Ocorrencias;
using CasaColomboApp.Services.Model.TipoOcorrencia;

namespace CasaColomboApp.Services.Model.Ocorrencia
{
    public class BaixaOcorrenciaGetModel
    {
        public int BaixaId { get; set; }
        public int CodProduto { get; set; }
        public string Produto { get; set; }
        public string NumeroNota { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        public int FornecedorGeralId { get; set; }
        public int LojaId   { get; set; }
        public int TipoOcorrenciaId { get; set; }
        public int OcorrenciaId { get; set; }
        public DateTime DataTime { get; set; }





    }
}
