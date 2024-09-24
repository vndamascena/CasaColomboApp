using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorGeral;
using CasaColomboApp.Services.Model.Loja;
using CasaColomboApp.Services.Model.Produto;
using CasaColomboApp.Services.Model.TipoOcorrencia;

namespace CasaColomboApp.Services.Model.Ocorrencias
{
    public class OcorrenciaGetModel
    {
        public int Id { get; set; }
        public int CodProduto { get; set; }
        public string Produto { get; set; }
        
        public string NumeroNota { get; set; }
        public DateTime DataTime { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        public LojaGetModel Loja { get; set; }
        public TipoOcorrenciaGetModel? TipoOcorrencia { get; set; }
        public FornecedorGeralGetModel? FornecedorGeral { get; set; }




    }
}
