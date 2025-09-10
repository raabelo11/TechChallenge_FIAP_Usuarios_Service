using Microsoft.EntityFrameworkCore;
using Usuarios.Service.Domain.Entities;

namespace Usuarios.Service.Infrastructure.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {

        }

        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new Configuration.UsuariosConfiguration());
        }
    }
}
