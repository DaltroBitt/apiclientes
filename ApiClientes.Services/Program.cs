using ApiClientes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//registrando as configurações do EntityFramework
EntityFrameworkConfiguration.Register(builder);
CorsConfiguration.Register(builder);
SwaggerConfiguration.Register(builder);


var app = builder.Build();

//ativando as configurações do projeto
CorsConfiguration.Use(app);
SwaggerConfiguration.Use(app);



app.UseAuthorization();

app.MapControllers();

app.Run();
