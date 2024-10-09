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
        ProdutoGeral Cadastrar(ProdutoGeral produtoGeral, List<(int depositoId, int quantidade)> depositosSelecionados, string matricula);

        ProdutoGeral Atualizar(ProdutoGeral produtoGeral, string matricula);
        ProdutoGeral Inativar(int id);

        List<ProdutoGeral> Consultar();
        ProdutoGeral ObterPorId(int id);

        // Métodos para gerenciar produtos e depósitos
        List<ProdutoDeposito> ConsultarQuantidadeProdutoDeposito(int produtoGeralId);
        void ExcluirQuantidadeProdutoDeposito(int produtoGeralId, int depositoId);

        // Métodos de venda
        void ConfirmarVenda(int id, int quantidadeVendida, string matricula);
        void UploadVenda(int id, int quantidadeVendida, string matricula, string DataVenda);

        void Excluir(int id);
    }
}
