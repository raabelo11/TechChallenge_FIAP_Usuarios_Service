using Usuarios.Service.Application.DTOs;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Domain.Entities;
using Usuarios.Service.Domain.Enums;
using Usuarios.Service.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

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

            var checkEmail = await _usuarioRepository.GetUser(usuarioDTO.Email);

            if (checkEmail != null)
            {
                return new GenericReturnDTO
                {
                    Success = false,
                    Error = "Email já cadastrado, por favor escolha outro!"
                };
            }

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

        public async Task<GenericReturnDTO> Login(UsuarioLoginDTO usuarioLoginDTO)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUser(usuarioLoginDTO.Email);
                if (usuario == null || !BCrypt.Net.BCrypt.Verify(usuarioLoginDTO.Senha, usuario.Senha))
                {
                    return new GenericReturnDTO
                    {
                        Success = false,
                        Error = "Email ou senha inválidos."
                    };
                }

                var token = GenerateToken(usuario);

                return new GenericReturnDTO
                {
                    Success = true,
                    Data = token
                };
            }
            catch (Exception ex)
            {
                return new GenericReturnDTO
                {
                    Success = false,
                    Error = ex.Message
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

        public string GenerateToken(Usuario usuario)
        {
            var secret = "SECRET_SECRETA_AQUI_7DFS78DFYFDSH7DFSHDFS7DFSD7FSHFDS87FHD";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim("Id", usuario.IdUsuario.ToString()),
                new Claim("Email", usuario.Email.ToString()),
                new Claim("NomeUsuario", usuario.NomeUsuario.ToString()),
                new Claim("DataCadastro", usuario.DataCadastro.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "JWT_ISSUER",
                audience: "JWT_AUDIENCE",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            string jwt = tokenHandler.WriteToken(token);

            return jwt;
        }
    }
}
