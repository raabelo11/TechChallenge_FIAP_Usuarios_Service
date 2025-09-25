using Microsoft.EntityFrameworkCore;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Application.UseCases;
using Usuarios.Service.Domain.Interfaces;
using Usuarios.Service.Infrastructure.EntityFramework;
using Usuarios.Service.Infrastructure.Repository;
using Serilog;
using Usuarios.ApiService.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Libera qualquer ip com a porta 8080 para AWS
builder.WebHost.UseUrls("http://0.0.0.0:8080");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosUseCase, UsuariosUseCase>();

// Configuração do Serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter()) // Console em JSON
    .WriteTo.File(new Serilog.Formatting.Json.JsonFormatter(),    // Arquivo em JSON
                  "logs/log.json",
                  rollingInterval: RollingInterval.Day)
    .CreateLogger();

// Substitui o logger padrão do .NET pelo Serilog
builder.Host.UseSerilog();

var app = builder.Build();

app.UseLogginMiddleware();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();