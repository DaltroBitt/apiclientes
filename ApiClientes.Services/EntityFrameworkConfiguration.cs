using ApiClientes.Infra.Data.Contexts;
using ApiClientes.Infra.Data.Interfaces;
using ApiClientes.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiClientes.Services
{
    /// <summary>
    /// Classe para configuração do EntityFramework
    /// </summary>
    public class EntityFrameworkConfiguration
    {
        /// <summary>
        /// Método para registrar a configuração
        /// </summary>
        public static void Register(WebApplicationBuilder builder)
        {
            //capturar a connectionstring do banco de dados
            var connectionString = builder.Configuration.GetConnectionString("ApiClientes");

            //injeção de dependencia para a classe 
            //SqlServerContext no EntityFramework

            builder.Services.AddDbContext<SqlServerContext>
                (map => map.UseSqlServer(connectionString));

            //mapear cada classe do repositorio
            builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
                        
        }
    }
}

