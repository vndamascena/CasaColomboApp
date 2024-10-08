﻿using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorGeral;
using System.ComponentModel.DataAnnotations;

namespace CasaColomboApp.Services.Model.Produto.Piso
{
    public class ProdutoPisoGetModel
    {
        public int? Id { get; set; }
       
        public string? Nome { get; set; }
        public string? Marca { get; set; }
        public int? Quantidade { get; set; }
        public string? Pei { get; set; }
        public string? Descricao { get; set; }

        public int? PecasCaixa { get; set; }

        public string? MetroQCaixa { get; set; }

        public string? PrecoCaixa { get; set; }

        
        public string? PrecoMetroQ { get; set; }

        public DateTime DataHoraCadastro { get; set; }
        public DateTime DataHoraAlteracao { get; set; }

        public FornecedorGeralGetModel? Fornecedor { get; set; }
        public CategoriaGetModel? Categoria { get; set; }

        public DepositoGetModel? Deposito { get; set; }

        public string? ImagemUrl { get; set; }
        public List<LoteGetModel> Lote { get; set; }

    }
}
