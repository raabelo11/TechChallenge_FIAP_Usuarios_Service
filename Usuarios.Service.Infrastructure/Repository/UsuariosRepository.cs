using Microsoft.EntityFrameworkCore;
using Usuarios.Service.Domain.Entities;
using Usuarios.Service.Domain.Enums;
using Usuarios.Service.Domain.Interfaces;
using Usuarios.Service.Infrastructure.EntityFramework;

namespace Usuarios.Service.Infrastructure.Repository
{
    public class UsuariosRepository(AppDbContext context) : IUsuariosRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<bool> Add(Usuario usuario)
        {
            await _context.AddAsync(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuario?> GetUser(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<List<Usuario>> List(int id, string? nome, string? email, DateTime? dataDe, DateTime? dataAte, bool ativo, TipoUsuario? tipo)
        {
            var ret = await _context.Usuarios
                .Where(Usuarios =>
                (id == 0 || Usuarios.IdUsuario == id) &&
                (string.IsNullOrEmpty(nome) || Usuarios.NomeUsuario.Contains(nome)) &&
                (dataDe == null || Usuarios.DataCadastro >= dataDe) &&
                (dataAte == null || Usuarios.DataCadastro <= dataAte) &&
                (string.IsNullOrEmpty(email) || Usuarios.Email.Contains(email)) &&
                (Usuarios.Ativo == ativo) &&
                (tipo == null || Usuarios.TipoUsuario == tipo)
            ).ToListAsync();

            return ret;
        }
    }
}
