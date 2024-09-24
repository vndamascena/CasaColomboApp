using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Fornecedor
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Cnpj { get; set; }

        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        #region Relacionamento

        public List<ProdutoPiso>? ProdutosPiso { get; set; }

        #endregion
    }
}
