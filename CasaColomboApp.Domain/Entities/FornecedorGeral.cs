using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class FornecedorGeral
    {
        public int? Id { get; set; }
        public string? Nome { get; set; }
        public string? Vendedor { get; set; }
        public string? ForneProdu { get; set; }
        public string? Tipo { get; set; }
        public string? TelVen { get; set; }
        public string? TelFor { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        #region Relacionamento

        public List<Ocorrencia>? Ocorrencia { get; set; }
        public List<ProdutoPiso>? ProdutosPiso { get; set; }

        public List<ProdutoGeral>? ProdutosGeral { get; set; }
        #endregion
    }
}
