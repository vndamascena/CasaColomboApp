﻿using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasaColomboApp.Domain.Services
{
    public class OcorrenciaDomainService : IOcorrenciaDomainService
    {
        private readonly IOcorrenciaRepository _ocorrenciaRepository;
        private readonly IBaixaOcorrenciaRepository _baixaOcorrenciaRepository;
        public OcorrenciaDomainService(IOcorrenciaRepository? ocorrenciaRepository, IBaixaOcorrenciaRepository? baixaOcorrenciaRepository)
        {
           _ocorrenciaRepository = ocorrenciaRepository;
            _baixaOcorrenciaRepository = baixaOcorrenciaRepository;
        }

       

        public Ocorrencia Cadastrar(Ocorrencia ocorrencia, string matricula)
        {

            ocorrencia.UsuarioId = matricula;

            _ocorrenciaRepository?.Add(ocorrencia);
           

            return ocorrencia;
        }


        //public Ocorrencia Atualizar(Ocorrencia ocorrencia)
        //{
          // var registro  = ObterPorId(ocorrencia.Id);
            //if (registro == null)
              //  throw new ApplicationException("Ocorrência não encontrado para edição.");

            

        //}

        public List<Ocorrencia> Consultar()
        {
           var ocorrencia = _ocorrenciaRepository?.GetAll();

            if (ocorrencia == null)
                return new List<Ocorrencia>();

            return ocorrencia.ToList();


        }

        public Ocorrencia ObterPorId(int id)
        {
           var ocorrencia = _ocorrenciaRepository?.GetById(id);

            return ocorrencia;
        }

        public void BaixaOcorrencia(int  id, string matricula)
        {
            var ocorrencia = _ocorrenciaRepository.GetById(id);

            if (ocorrencia == null)
            {

                throw new ApplicationException("Ocorrência não encontrado.");
            }

            var baixaOcorrencia = new BaixaOcorrencia
            {
                TipoOcorrenciaId = ocorrencia.Id,
                NumeroNota = ocorrencia.NumeroNota,
                CodProduto = ocorrencia.CodProduto,
                UsuarioId = matricula,
                Fornecedo = ocorrencia.Fornecedo,
                Produto = ocorrencia.Produto,
                Observacao = ocorrencia.Observacao,
                DataTime = DateTime.Now
                
            };

            _baixaOcorrenciaRepository.Add(baixaOcorrencia);
           
        }

     

       
    }
}