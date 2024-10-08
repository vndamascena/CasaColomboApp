﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Entities
{
    public class Ocorrencia
    {
        public int Id { get; set; }
        public int CodProduto { get; set; }

        public bool Ativo { get; set; }
        public string Produto { get; set; }
        public int FornecedorGeralId { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataTime { get; set; }
        public string Observacao { get; set; }
        public string UsuarioId { get; set; }
        public int LojaId { get; set; }
        public int TipoOcorrenciaId { get; set; }

        public Loja? Loja { get; set; }

        public List<BaixaOcorrencia> BaixaOcorrencias {  get; set; }
        public TipoOcorrencia? TipoOcorrencia { get; set; }
        public FornecedorGeral? FornecedorGeral { get; set; }

        public Ocorrencia() 
        {
            BaixaOcorrencias = new List<BaixaOcorrencia>();

        }
    }
}
