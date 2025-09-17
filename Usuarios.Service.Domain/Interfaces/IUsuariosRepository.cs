using Usuarios.Service.Domain.Entities;
using Usuarios.Service.Domain.Enums;

namespace Usuarios.Service.Domain.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<bool> Add(Usuario usuario);
        Task<Usuario?> GetUser(string email);
        Task<List<Usuario>> List(int id, string? nome, string? email, DateTime? dataDe, DateTime? dataAte, bool ativo, TipoUsuario? tipo);
    }
}
