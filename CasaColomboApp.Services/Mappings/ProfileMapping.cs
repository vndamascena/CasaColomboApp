﻿using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorOcorrencia;
using CasaColomboApp.Services.Model.Loja;
using CasaColomboApp.Services.Model.Ocorrencia;
using CasaColomboApp.Services.Model.Ocorrencias;
using CasaColomboApp.Services.Model.Produto;
using CasaColomboApp.Services.Model.TipoOcorrencia;

namespace CasaColomboApp.Services.Mappings
{
    /// <summary>
    /// Classe para configuração dos mapeamentos
    /// feitos no projeto através do AutoMapper
    /// </summary>
    public class ProfileMapping : Profile
    {

        /// <summary>
        /// Construtor
        /// </summary>
        public ProfileMapping()
        {
            //mapeamento de ProdutosPostModel para Produto
            CreateMap<ProdutoPostModel, Produto>()
                .AfterMap((model, entity) =>
                {
                    
                    entity.DataHoraCadastro = DateTime.Now;
                    entity.DataHoraAlteracao = DateTime.Now;
                    entity.Ativo = true;
                });

            CreateMap<CategoriaPostModel, Categoria>()
                .AfterMap((model, entity) =>
                {
                   
                    entity.DataHoraCadastro = DateTime.Now;
                    entity.DataHoraAlteracao = DateTime.Now;

                });
            CreateMap<OcorrenciaPostModel, Ocorrencia>()
               .AfterMap((model, entity) =>
               {

                   entity.DataTime = DateTime.Now;
                   entity.Ativo = true;


               });

            CreateMap<FornecedorPostModel, Fornecedor>()
               .AfterMap((model, entity) =>
               {

                   entity.DataHoraCadastro = DateTime.Now;
                   entity.DataHoraAlteracao = DateTime.Now;

               });
            CreateMap<FornecedorOcorrenciaPostModel, FornecedorOcorrencia>()
               .AfterMap((model, entity) =>
               {

                   entity.DataHoraCadastro = DateTime.Now;
                   entity.DataHoraAlteracao = DateTime.Now;

               });
            CreateMap<Deposito, DepositoGetModel>();


            CreateMap<Categoria, CategoriaGetModel>();

           
            CreateMap<Ocorrencia, OcorrenciaGetModel>()
                .ForMember(dest => dest.TipoOcorrencia, opt => opt.MapFrom(src => src.TipoOcorrencia))
                .ForMember(dest => dest.FornecedorOcorrencia, opt => opt.MapFrom(src => src.FornecedorOcorrencia))
                .ForMember(dest => dest.Loja, opt => opt.MapFrom(src => src.Loja));


            CreateMap<Fornecedor, FornecedorGetModel>();
            CreateMap<FornecedorOcorrencia, FornecedorOcorrenciaGetModel>();

            CreateMap<Produto, ProdutoGetModel>()
             .ForMember(dest => dest.Lote, opt => opt.MapFrom(src => src.Lote));

            CreateMap<Lote, LoteGetModel>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.NumeroLote, opt => opt.MapFrom(src => src.NumeroLote))
             .ForMember(dest => dest.QuantidadeLote, opt => opt.MapFrom(src => src.QuantidadeLote));



            CreateMap<LoteModel, Lote>();
            CreateMap<Lote, LoteModel>();

            CreateMap<ProdutoPutModel, Produto>()
             .ForMember(dest => dest.Lote, opt => opt.MapFrom(src => src.Lote));
            CreateMap<LoteGetModel, LoteModel>();
            CreateMap<LoteGetModel, Lote>();
            CreateMap<Venda, VendaGetModel>();
            CreateMap<BaixaOcorrencia, BaixaOcorrenciaGetModel>();

            CreateMap<TipoOcorrenciaPostModel, TipoOcorrencia>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaPostModel>();
            CreateMap<TipoOcorrenciaPutModel, TipoOcorrencia>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaPutModel>();

            CreateMap<OcorrenciaPostModel, Ocorrencia>();
            CreateMap<Ocorrencia, OcorrenciaPostModel>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaGetModel>();
            CreateMap<FornecedorOcorrencia, FornecedorOcorrenciaPostModel>();
            CreateMap<FornecedorOcorrenciaPostModel, FornecedorOcorrencia>();
            CreateMap<Loja, LojaGetModel>();
            CreateMap<Loja, LojaPostModel>();
            CreateMap<LojaGetModel, Loja>();
            CreateMap<LojaPostModel, Loja>();












        }


    }
}
