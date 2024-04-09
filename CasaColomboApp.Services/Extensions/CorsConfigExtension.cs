namespace CasaColomboApp.Services.Extensions
{
    public class CorsConfigExtension
    {
        //nome da política de acesso (CORS)
        private static string _policyName = "DefaultPolicy";

        //método para definir as permissões do CORS
        public static void AddCorsConfig(IServiceCollection services)
        {
            services.AddCors(config => config.AddPolicy(_policyName, builder =>
            {
                //qualquer domínio poderá fazer requisições para a API
                builder.AllowAnyOrigin()
                //permissão para qualquer requisição (POST, PUT, DELETE, GET etc)
                .AllowAnyMethod()
                //permissão para enviar parametros de cabeçalho na requisição
                .AllowAnyHeader();
            }));
        }

        //método para usar as permissões
        public static void UseCorsConfig(IApplicationBuilder app)
        {
            app.UseCors(_policyName);
        }
    }
}
