using Microsoft.OpenApi.Models;

namespace ApiClientes.Services
{
    /// <summary>
    /// Classe para configuração do Swagger
    /// </summary>
    public class SwaggerConfiguration
    {
        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            if (builder.Services == null) throw new ArgumentNullException(nameof(builder.Services));

            builder.Services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API para gerenciamento de clientes",
                    Description = "Treinamento .NET COTI Informática",
                    Contact = new OpenApiContact { Name = "COTI Informática", Email = "contato@cotiinformatica.com.br", Url = new Uri("http://www.cotiinformatica.com.br") }
                });
                                
            });
        }

        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Use(WebApplication app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiClientes");
            });
        }
    }
}

