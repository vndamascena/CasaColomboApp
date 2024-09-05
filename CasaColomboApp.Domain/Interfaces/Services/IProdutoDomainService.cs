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

        Produto Cadastrar(Produto produto, List<Lote>lotes, string matricula);
        Produto Atualizar(Produto produto,  string matricula);
        Produto Inativar(int id);

        List<Produto> Consultar();

        List<Lote> ConsultarLote(int produtoId);

        Produto ObterPorId(int id);
        void ExcluirLote(int produtoId,  int loteId);
        void ConfirmarVenda(int id,  int quantidadeVendida, string matricula);



    }
}
