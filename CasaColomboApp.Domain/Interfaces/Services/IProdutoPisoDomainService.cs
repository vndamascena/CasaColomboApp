using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IProdutoPisoDomainService
    {

        ProdutoPiso Cadastrar(ProdutoPiso produtoPiso, List<Lote>lotes, string matricula);
        ProdutoPiso Atualizar(ProdutoPiso produtoPiso,  string matricula);
        ProdutoPiso Inativar(int id);

        List<ProdutoPiso> Consultar();

        List<Lote> ConsultarLote(int produtoPisoId);

        ProdutoPiso ObterPorId(int id);
        void ExcluirLote(int produtoPisoId,  int loteId);
        void ConfirmarVenda(int id,  int quantidadeVendida, string matricula);



    }
}
