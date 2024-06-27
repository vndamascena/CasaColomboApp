﻿using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorOcorrencia;
using CasaColomboApp.Services.Model.Produto;
using CasaColomboApp.Services.Model.TipoOcorrencia;

namespace CasaColomboApp.Services.Model.Ocorrencias
{
    public class OcorrenciaGetModel
    {
        public int Id { get; set; }
        public int CodProduto { get; set; }
        public string Produto { get; set; }
        public FornecedorOcorrenciaGetModel? FornecedorOcorrencia { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataTime { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        public TipoOcorrenciaGetModel? TipoOcorrencia { get; set; }

    
       

    }
}
