using CasaColomboApp.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
SwaggerExtension.AddSwaggerConfig(builder.Services);
ServicesExtension.AddServicesConfig(builder.Services);
CorsConfigExtension.AddCorsConfig(builder.Services);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpClient();

var app = builder.Build();



SwaggerExtension.UseSwaggerConfig(app);
CorsConfigExtension.UseCorsConfig(app);
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program { }
