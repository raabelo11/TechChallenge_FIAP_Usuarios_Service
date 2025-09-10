using Usuarios.Service.Application.DTOs;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Domain.Entities;
using Usuarios.Service.Domain.Enums;
using Usuarios.Service.Domain.Interfaces;

namespace Usuarios.Service.Application.UseCases
{
    public class UsuariosUseCase(IUsuariosRepository usuarioRepository) : IUsuariosUseCase
    {
        private readonly IUsuariosRepository _usuarioRepository = usuarioRepository;

        public async Task<GenericReturnDTO> CadastraUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario(usuarioDTO.NomeUsuario,
                                      usuarioDTO.Email,
                                      BCrypt.Net.BCrypt.HashPassword(usuarioDTO.Senha),
                                      usuarioDTO.TipoUsuario);

            bool save = await _usuarioRepository.Add(usuario);

            if (save)
            {
                return new GenericReturnDTO
                {
                    Success = true
                };
            }
            else
            {
                return new GenericReturnDTO
                {
                    Success = false,
                    Error = "Erro ao cadastrar usuário"
                };
            }
        }

        public async Task<GenericReturnDTO> ListaUsuarios(int id, string? nome, string? email, DateTime? dataDe, DateTime? dataAte, bool ativo, TipoUsuario? tipo)
        {
            var usuario = await _usuarioRepository.List(id, nome, email, dataDe, dataAte, ativo, tipo);

            if (usuario != null)
                return new GenericReturnDTO
                {
                    Success = true,
                    Data = usuario
                };

            return new GenericReturnDTO
            {
                Success = true,
                Data = "Nenhum usuário encontrado"
            };
        }
    }
}
