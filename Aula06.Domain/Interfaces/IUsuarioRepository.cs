using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula06.Domain.Entities;

namespace Aula06.Domain.Interfaces
{
    public interface IUsuarioRepository
    {

        Task<Usuario> LogarAsync(string pUsuario, string pSenha);
        Task CriarUsuario(Usuario pUsuario);
        Task AtualizarUsuarioAsync(Usuario pUsuario);
        Task ExcluirUsuarioAsync(Usuario pUsuario);
        Task<Usuario> RetornarUsuarioPorCodigoAsync(int pId);
    }
}
