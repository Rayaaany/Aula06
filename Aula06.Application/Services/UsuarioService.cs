using Aula06.Domain.Entities;
using Aula06.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Aula06.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> AtualizarUsuarioAsync(Usuario pUsuario)
        {
            var usuarioExistente = await _usuarioRepository.RetornarUsuarioPorCodigoAsync(pUsuario.Id);
            if (usuarioExistente == null) return false;

            // Atualiza senha com hash novamente, se necessário
            if (!string.IsNullOrEmpty(pUsuario.Senha))
                pUsuario.Senha = GerarHashSHA256(pUsuario.Senha);

            await _usuarioRepository.AtualizarUsuarioAsync(pUsuario);
            return true;
        }

        public async Task<Usuario> CriarUsuario(Usuario pUsuario)
        {
            pUsuario.Senha = GerarHashSHA256(pUsuario.Senha);
            await _usuarioRepository.CriarUsuario(pUsuario);
            return pUsuario;
        }

        public async Task<bool> ExcluirUsuarioAsync(Usuario pUsuario)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorCodigoAsync(pUsuario.Id);
            if (usuario == null || usuario.Id <= 0)
                return false;

            await _usuarioRepository.ExcluirUsuarioAsync(pUsuario);
            return true;
        }

        public async Task<Usuario?> LogarAsync(string pUsuario, string pSenha)
        {
            string senhaHash = GerarHashSHA256(pSenha);
            var usuario = await _usuarioRepository.LogarAsync(pUsuario, senhaHash);

            if (usuario == null || usuario.Id <= 0)
                return null;

            return usuario;
        }

        public async Task<Usuario> RetornarUsuarioPorCodigoAsync(int pId)
        {
            return await _usuarioRepository.RetornarUsuarioPorCodigoAsync(pId);
        }

        private string GerarHashSHA256(string entrada)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(entrada));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
