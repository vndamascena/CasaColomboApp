using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Fornecedor.FornecedorOcorrencia
{
    public class FornecedorOcorrenciaPostModel
    {

        [Required(ErrorMessage = "Informe o nome do fornecedor.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(15, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }

       
    }
}
