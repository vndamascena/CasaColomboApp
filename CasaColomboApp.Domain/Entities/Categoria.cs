using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CasaColomboApp.Domain.Entities
{
    public class Categoria
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        #region Relacionamentos

        public List<Produto>? Produtos { get; set; }

        #endregion


    }
}
