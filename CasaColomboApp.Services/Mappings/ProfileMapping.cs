using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Fornecedor.FornecedorGeral;
using CasaColomboApp.Services.Model.Loja;
using CasaColomboApp.Services.Model.Ocorrencia;
using CasaColomboApp.Services.Model.Ocorrencias;
using CasaColomboApp.Services.Model.Produto;
using CasaColomboApp.Services.Model.Produto.Piso;
using CasaColomboApp.Services.Model.ProdutoGeral;
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
            CreateMap<ProdutoPisoPostModel, ProdutoPiso>()
                .AfterMap((model, entity) =>
                {
                    
                    entity.DataHoraCadastro = DateTime.Now;
                    entity.DataHoraAlteracao = DateTime.Now;
                    entity.Ativo = true;
                });
            CreateMap<ProdutoGeralPostModel, ProdutoGeral>()
               .AfterMap((model, entity) =>
               {

                   entity.DataHoraCadastro = DateTime.Now;
                   entity.DataHoraAlteracao = DateTime.Now;
                   
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
            CreateMap<FornecedorGeralPostModel, FornecedorGeral>()
               .AfterMap((model, entity) =>
               {

                   entity.DataHoraCadastro = DateTime.Now;
                   entity.DataHoraAlteracao = DateTime.Now;

               });
            CreateMap<Deposito, DepositoGetModel>();


            CreateMap<Categoria, CategoriaGetModel>();

           
            CreateMap<Ocorrencia, OcorrenciaGetModel>()
                .ForMember(dest => dest.TipoOcorrencia, opt => opt.MapFrom(src => src.TipoOcorrencia))
                .ForMember(dest => dest.FornecedorGeral, opt => opt.MapFrom(src => src.FornecedorGeral))
                .ForMember(dest => dest.Loja, opt => opt.MapFrom(src => src.Loja));


            CreateMap<Fornecedor, FornecedorGetModel>();
            CreateMap<FornecedorGeral, FornecedorGeralGetModel>();

            CreateMap<ProdutoPiso, ProdutoPisoGetModel>()
             .ForMember(dest => dest.Lote, opt => opt.MapFrom(src => src.Lote));
            CreateMap<ProdutoGeral, ProdutoGeralGetModel>()
                .ForMember(dest => dest.QuantidadeProdutoDeposito, opt => opt.MapFrom(src => src.QuantidadeProdutoDeposito));

            CreateMap<Lote, LoteGetModel>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.NumeroLote, opt => opt.MapFrom(src => src.NumeroLote))
             .ForMember(dest => dest.QuantidadeLote, opt => opt.MapFrom(src => src.QuantidadeLote));



            CreateMap<LoteModel, Lote>();
            CreateMap<Lote, LoteModel>();

            CreateMap<ProdutoPisoPutModel, ProdutoPiso>()
             .ForMember(dest => dest.Lote, opt => opt.MapFrom(src => src.Lote));
            CreateMap<ProdutoGeralPutModel, ProdutoGeral>()
             .ForMember(dest => dest.QuantidadeProdutoDeposito, opt => opt.MapFrom(src => src.QuantidadeProdutoDeposito));
            CreateMap<LoteGetModel, LoteModel>();
            CreateMap<LoteGetModel, Lote>();
            CreateMap<Venda, VendaPisoGetModel>();
            CreateMap<VendaProdutoGeral, VendaProdutoGeralGetModel>();
            CreateMap<BaixaOcorrencia, BaixaOcorrenciaGetModel>();

            CreateMap<TipoOcorrenciaPostModel, TipoOcorrencia>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaPostModel>();
            CreateMap<TipoOcorrenciaPutModel, TipoOcorrencia>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaPutModel>();

            CreateMap<OcorrenciaPostModel, Ocorrencia>();
            CreateMap<Ocorrencia, OcorrenciaPostModel>();
            CreateMap<TipoOcorrencia, TipoOcorrenciaGetModel>();
            CreateMap<FornecedorGeral, FornecedorGeralPostModel>();
            CreateMap<FornecedorGeralPostModel, FornecedorGeral>();
            CreateMap<Loja, LojaGetModel>();
            CreateMap<Loja, LojaPostModel>();
            CreateMap<LojaGetModel, Loja>();
            CreateMap<LojaPostModel, Loja>();

            CreateMap<QuantidadeProdutosDepositosPostModel, QuantidadeProdutosDepositos>();
            CreateMap<QuantidadeProdutosDepositosGetModel, QuantidadeProdutosDepositosPostModel>();
            CreateMap<QuantidadeProdutosDepositosGetModel, QuantidadeProdutosDepositos>();
            CreateMap<QuantidadeProdutosDepositos, QuantidadeProdutosDepositosPostModel>();
            CreateMap<QuantidadeProdutosDepositos, QuantidadeProdutosDepositosGetModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));












        }


    }
}
