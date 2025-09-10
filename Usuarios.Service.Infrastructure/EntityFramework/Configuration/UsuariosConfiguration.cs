using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Service.Domain.Entities;

namespace Usuarios.Service.Infrastructure.EntityFramework.Configuration
{
    public class UsuariosConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.IdUsuario)
           .ValueGeneratedOnAdd()
           .UseIdentityColumn();

            builder.Property(u => u.NomeUsuario)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(u => u.TipoUsuario)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(u => u.DataCadastro)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(u => u.Ativo)
                .IsRequired()
                .HasColumnType("bit")
                .HasDefaultValue(true);
        }
    }
}
