using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula06.Domain.Entities;

namespace Aula06.Domain.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> LogarAsync(string pUsuario, string pSenha);
        Task<Usuario> CriarUsuario(Usuario pUsuario);
        Task <bool> AtualizarUsuarioAsync(Usuario pUsuario);
        Task <bool> ExcluirUsuarioAsync(Usuario pUsuario);
        Task<Usuario> RetornarUsuarioPorCodigoAsync(int pId);
    }
}

