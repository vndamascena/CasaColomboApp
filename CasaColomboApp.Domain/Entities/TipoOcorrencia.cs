using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class TipoOcorrencia
    {
       
        public int? Id { get; set; }

        
        public string? Nome { get; set; }
        public List<Ocorrencia>? Ocorrencia { get; set; }
    }
}
