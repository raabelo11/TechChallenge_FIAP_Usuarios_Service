using Usuarios.Service.Domain.Enums;

namespace Usuarios.Service.Application.DTOs
{
    public class UsuarioDTO
    {
        public required string NomeUsuario { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }

    public class UsuarioLoginDTO
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
