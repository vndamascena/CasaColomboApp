using CasaColomboApp.Domain.Entities;
using CasaColomboApp.Domain.Interfaces.Repositories;
using CasaColomboApp.Domain.Interfaces.Services;
using CasaColomboApp.Infra.Data.Repositories;
using CasaColomboApp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CasaColomboApp.Services.Extensions
{
    public class ServicesExtension
    {
        public static void AddServicesConfig(IServiceCollection services)
        {
            services.AddTransient<IFornecedorDomainService, FornecedorDomainService>();
            services.AddTransient<ICategoriaDomainService, CategoriaDomainService>();
            services.AddTransient<IProdutoPisoDomainService, ProdutoPisoDomainService>();
            services.AddTransient<IFornecedorGeralDomainService, FornecedorGeralDomainService>();
            services.AddTransient<IDepositoDomainService, DepositoDomainService>();
            services.AddTransient<IOcorrenciaDomainService, OcorrenciaDomainService>();
            services.AddTransient<ITipoOcorrenciaDomainService, TipoOcorrenciaDomainService>();
            services.AddTransient<ILojaDomainService, LojaDomainService>();
            services.AddTransient<IProdutoGeralDomainService, ProdutoGeralDomainService>();


            services.AddTransient<IFornecedorRepository, FornecedorRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
            services.AddTransient<IProdutoPisoRepository, ProdutoPisoRepository>();
            services.AddTransient<IVendaProdutoGeralRepository, VendaProdutoGeralRepository>();
            services.AddTransient<IDepositoRepository, DepositoRepository>();
            services.AddTransient<ILoteRepository, LoteRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();
            services.AddTransient<ITipoOcorrenciaRepository, TipoOcorrenciaRepository>();
            services.AddTransient<IOcorrenciaRepository, OcorrenciaRepository>();
            services.AddTransient<IBaixaOcorrenciaRepository, BaixaOcorrenciaRepository>();
            services.AddTransient<IFornecedorGeralRepository, FornecedorGeralRepository>();
            services.AddTransient<ILojaRepository, LojaRepository>();
            services.AddTransient<IProdutoGeralRepository, ProdutoGeralRepository>();
            services.AddTransient<IQuantidadeProdutosDepositosRepository, QuantidadeProdutosDepositosRepository>();

        }
    }
}
