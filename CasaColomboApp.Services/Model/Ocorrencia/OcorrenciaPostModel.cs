using CasaColomboApp.Services.Model.TipoOcorrencia;
using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Ocorrencias
{
    public class OcorrenciaPostModel
    {
       
        public int CodProduto { get; set; }

        [Required(ErrorMessage = "Informe o nome do produto.")]
        [MinLength(1, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no maximo {1} carateres.")] 
        public string Produto { get; set; }
        public int? FornecedorOcorrenciaId { get; set; }
        public string NumeroNota { get; set; }
       
        public string Observacao { get; set; }


        [Required(ErrorMessage = "Por favor, informeo ID da ocorrencia para o produto.")]
        public int? TipoOcorrenciaId { get; set; }
     


    }
}
