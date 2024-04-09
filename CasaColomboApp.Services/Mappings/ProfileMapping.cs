using AutoMapper;
using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Services.Model.Categoria;
using CasaColomboApp.Services.Model.Deposito;
using CasaColomboApp.Services.Model.Fornecedor;
using CasaColomboApp.Services.Model.Produto;

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
                    entity.Id = Guid.NewGuid();
                    entity.DataHoraCadastro = DateTime.Now;
                    entity.DataHoraAlteracao = DateTime.Now;
                    entity.Ativo = true;
                });

            CreateMap<CategoriaPostModel, Categoria>()
                .AfterMap((model, entity) =>
                {
                    entity.Id = Guid.NewGuid();
                    entity.DataHoraCadastro = DateTime.Now;
                    entity.DataHoraAlteracao = DateTime.Now;

                });

            CreateMap<FornecedorPostModel, Fornecedor>()
               .AfterMap((model, entity) =>
               {
                   entity.Id = Guid.NewGuid();
                   entity.DataHoraCadastro = DateTime.Now;
                   entity.DataHoraAlteracao = DateTime.Now;

               });
            CreateMap<Deposito, DepositoGetModel>();
           

            CreateMap<Categoria, CategoriaGetModel>();

            CreateMap<Fornecedor, FornecedorGetModel>();

            CreateMap<Produto, ProdutoGetModel>();
                
        }
    }
}
