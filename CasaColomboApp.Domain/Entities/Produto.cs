using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Produto
    {
        public Guid? Id { get; set; }

        public string? Codigo { get; set; }
        public string Nome { get; set; }

        public string? Marca { get; set; }

        public string? Quantidade { get; set; }

        public string? Lote { get; set; }

        public string? Descricao { get; set; }

        public DateTime? DataHoraCadastro { get; set; }

        public DateTime? DataHoraAlteracao { get; set; }
        public bool Ativo { get; set; }
        public Guid? CategoriaId { get; set; }

        public Guid? FornecedorId { get; set; }

        public int? DepositoId { get; set; }

        




        #region Relacionamentos

        public Categoria? Categoria { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public Deposito? Deposito { get; set; }

        
        #endregion

    }
}
