using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Produto
    {
        public int Id { get; set; }

        
        public string Nome { get; set; }

        public string? Marca { get; set; }

        public int? Quantidade { get; set; }

        public string? Pei { get; set; }

        public string? Descricao { get; set; }

        public int? PecasCaixa { get; set; }

        public string? MetroQCaixa { get; set; }

       
        public decimal? PrecoCaixa { get; set; }

        
        public decimal? PrecoMetroQ {  get; set; }

        public DateTime DataHoraCadastro { get; set; }

        public DateTime DataHoraAlteracao { get; set; }
        public bool Ativo { get; set; }
        public int? CategoriaId { get; set; }

        public int? FornecedorId { get; set; }

        public int? DepositoId { get; set; }
        public string? ImagemUrl { get; set; }
       


        #region Relacionamentos

        public Categoria? Categoria { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public Deposito? Deposito { get; set; }
        public List<Lote>? Lote { get; set; } // Lista de lotes associados ao produto
    }


        #endregion

}



