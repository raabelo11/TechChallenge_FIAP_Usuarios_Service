using Microsoft.EntityFrameworkCore;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Application.UseCases;
using Usuarios.Service.Domain.Interfaces;
using Usuarios.Service.Infrastructure.EntityFramework;
using Usuarios.Service.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

//Libera qualquer ip com a porta 8080 para AWS
builder.WebHost.UseUrls("http://0.0.0.0:8080");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// DI
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosUseCase, UsuariosUseCase>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();