using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class QuantidadeProdutosDepositos
    {
        public int Id { get; set; }
        public int Quantidade{ get; set; }
        public int ProdutoGeralId { get; set; }
        public string? UsuarioId { get; set; }
        public string? NomeDeposito { get; set; }
        public string? NomeProduto { get; set; }
        public string? CodigoSistema { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataUltimaAlteracao { get; set; }
        public ProdutoGeral ProdutoGeral { get; set; }
       
        public QuantidadeProdutosDepositos()
        {
            DataEntrada = DateTime.Now;
            DataUltimaAlteracao = DateTime.Now;
        }
    }
}
