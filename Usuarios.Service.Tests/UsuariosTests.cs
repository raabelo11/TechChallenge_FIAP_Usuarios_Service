using Microsoft.Extensions.Logging;
using Moq;
using Usuarios.Service.Application.DTOs;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Application.UseCases;
using Usuarios.Service.Domain.Entities;
using Usuarios.Service.Domain.Enums;
using Usuarios.Service.Domain.Interfaces;

namespace Usuarios.Service.Tests
{
    public class UsuariosTests
    {
        [Fact(DisplayName = "Dado que o e-mail já existe, quando tentar criar um usuário, então deve retornar mensagem de e-mail duplicado")]
        public async Task ValidarCriacaoDeUsuarioComEmailJaExistenteNaBaseDeDados()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO
            {
                NomeUsuario = "Guilherme Magalhães",
                Email = "guilherme2025@gmail.com",
                Senha = "criacaoABDESS123@@",
                TipoUsuario = TipoUsuario.Usuario
            };

            var usuarioExistente = new Usuario(
                usuarioDTO.NomeUsuario,
                usuarioDTO.Email,
                usuarioDTO.Senha,
                usuarioDTO.TipoUsuario
            );

            var mockLog = new Mock<ILogger<UsuariosUseCase>>();
            var mockRepository = new Mock<IUsuariosRepository>();
            mockRepository.Setup(r => r.GetUser(usuarioDTO.Email)).ReturnsAsync(usuarioExistente);

            var useCase = new UsuariosUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.CadastraUsuario(usuarioDTO);

            // Assert
            Assert.False(result.Success);
            Assert.Equal("Email já cadastrado, por favor escolha outro!", result.Error);
        }

        [Fact(DisplayName = "Dado que os dados do usuário são válidos, quando criar o usuário, então deve retornar sucesso")]
        public async Task CriarUsuarioComSucesso()
        {
            // Arrange
            var usuarioDTO = new UsuarioDTO
            {
                NomeUsuario = "Guilherme Lima",
                Email = "guilherme@gmail.com",
                Senha = "senhaTESTE123@@",
                TipoUsuario = TipoUsuario.Usuario
            };

            var mockLog = new Mock<ILogger<UsuariosUseCase>>();
            var mockRepository = new Mock<IUsuariosRepository>();
            mockRepository.Setup(r => r.GetUser(usuarioDTO.Email)).ReturnsAsync((Usuario?)null);
            mockRepository.Setup(r => r.Add(It.IsAny<Usuario>())).ReturnsAsync(true);

            var useCase = new UsuariosUseCase(mockRepository.Object, mockLog.Object);

            // Act
            var result = await useCase.CadastraUsuario(usuarioDTO);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.Error);
        }
    }
}