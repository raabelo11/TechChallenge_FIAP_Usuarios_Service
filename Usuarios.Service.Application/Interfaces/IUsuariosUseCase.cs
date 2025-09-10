using Usuarios.Service.Application.DTOs;
using Usuarios.Service.Domain.Enums;

namespace Usuarios.Service.Application.Interfaces
{
    public interface IUsuariosUseCase
    {
        Task<GenericReturnDTO> CadastraUsuario(UsuarioDTO usuarioDTO);
        Task<GenericReturnDTO> ListaUsuarios(int id, string? nome, string? email, DateTime? dataDe, DateTime? dataAte, bool ativo, TipoUsuario? tipo);
    }
}