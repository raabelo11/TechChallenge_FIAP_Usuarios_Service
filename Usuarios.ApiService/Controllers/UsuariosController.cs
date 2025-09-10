using Microsoft.AspNetCore.Mvc;
using Usuarios.Service.Application.DTOs;
using Usuarios.Service.Application.Interfaces;
using Usuarios.Service.Domain.Enums;

namespace Usuarios.ApiService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController(IUsuariosUseCase usuariosUseCase) : Controller
    {
        private readonly IUsuariosUseCase _usuariosUseCase = usuariosUseCase;

        [HttpPost]
        [Route("Cadastro")]
        [Consumes("application/json")]
        public async Task<ActionResult> Cadastro(UsuarioDTO usuarioDTO)
        {
            var ret = await _usuariosUseCase.CadastraUsuario(usuarioDTO);
            return ret.Success ? Ok(ret) : BadRequest(ret);
        }

        [HttpGet]
        [Route("Lista")]
        [Consumes("application/json")]
        public async Task<ActionResult> Lista(int id, string? nome, string? email, DateTime? dataDe, DateTime? dataAte, TipoUsuario? tipo, bool ativo = true)
        {
            var ret = await _usuariosUseCase.ListaUsuarios(id, nome, email, dataDe, dataAte, ativo, tipo);
            return ret.Success ? Ok(ret) : BadRequest(ret);
        }
    }
}
