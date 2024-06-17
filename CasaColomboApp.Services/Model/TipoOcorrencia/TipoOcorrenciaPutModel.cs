using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.TipoOcorrencia
{
    public class TipoOcorrenciaPutModel
    {

        [Required(ErrorMessage = "Por favor, informe o id do ocorrência.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o nome da ocorrência.")]
        [MinLength(4, ErrorMessage = "Informe no minimo {1} caracteres.")]
        [MaxLength(50, ErrorMessage = "Informe no maximo {1} carateres.")]
        public string? Nome { get; set; }
    }
}
