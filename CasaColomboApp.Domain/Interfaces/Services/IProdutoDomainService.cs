using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IProdutoDomainService
    {

        Produto Cadastrar(Produto produto, List<Lote>lotes);
        Produto Atualizar(Produto produto);
        Produto Inativar(Guid id);

        List<Produto> Consultar();

        List<Lote> ConsultarLote(Guid produtoId);

        Produto ObterPorId(Guid id);
        void ExcluirLote(Guid produtoId, Guid loteId);
        void ConfirmarVenda(Guid id,  int quantidadeVendida);



    }
}
