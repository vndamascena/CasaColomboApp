using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class BaixaOcorrencia
    {
        public int BaixaId { get; set; }
        public int CodProduto { get; set; }
        public int OcorrenciaId { get; set; }
        public int TipoOcorrenciaId { get; set; }
       
        public string Produto { get; set; }
        public int FornecedorOcorrenciaId { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataTime { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        

        public Ocorrencia? Ocorrencia { get; set; }

    }
}
