using CasaColomboApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Interfaces.Services
{
    public interface IProdutoGeralDomainService
    {
        ProdutoGeral Cadastrar(ProdutoGeral produtoGeral, List<QuantidadeProdutosDepositos> quantidadeProdutosDepositos, string matricula);

        ProdutoGeral Atualizar(ProdutoGeral produtoGeral, string matricula);
        ProdutoGeral Inativar(int id);

        List<ProdutoGeral> Consultar();

        List<QuantidadeProdutosDepositos> ConsultarQuantidadeProdutoDeposito(int produtoGeralId);

        ProdutoGeral ObterPorId(int id);
        void ExcluirQuantidadeProdutoDeposito(int produtoGeralId, int quantidadeProdutoDepositoId);
        void ConfirmarVenda(int id, int quantidadeVendida, string matricula);

        void Excluir(int id);

    }
}
