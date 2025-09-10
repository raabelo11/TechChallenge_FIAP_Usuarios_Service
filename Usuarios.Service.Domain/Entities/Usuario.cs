using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.Service.Domain.Enums;

namespace Usuarios.Service.Domain.Entities
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool? Ativo { get; set; }

        // Construtor de domínio
        public Usuario(string nomeUsuarioDTO, string email, string senha, TipoUsuario tipoUsuario)
        {
            this.NomeUsuario = nomeUsuarioDTO;
            this.Email = email;
            this.Senha = senha;
            this.TipoUsuario = tipoUsuario;
        }

        // Construtor vazio para EF
        private Usuario() { }
    }
}
