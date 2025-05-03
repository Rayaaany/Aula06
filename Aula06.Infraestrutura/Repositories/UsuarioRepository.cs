using Aula06.Infraestrutura.Infraestrutura;
using AulaFinal.Domain.Entities;
using AulaFinal.Domain.Interfaces;
using AulaFinal.Infraestrutura.Data;
using Microsoft.EntityFrameworkCore;

namespace AulaFinal.Infraestrutura.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SistemaFinalContext _context;

        public UsuarioRepository(SistemaFinalContext context)
        {
            _context = context;
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario); // Usando AddAsync para operações assíncronas
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario> LogarAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios
                                         .FirstOrDefaultAsync(x => x.Email == email && x.Senha == senha);

            return usuario; // Retorna 'null' se não encontrar, sem necessidade de '?? null'
        }

        public async Task<Usuario> RetornarUsuarioPorCodigoAsync(int id)
        {
            var usuario = await _context.Usuarios
                                         .FirstOrDefaultAsync(x => x.Id == id);

            return usuario; // Retorna 'null' se não encontrar, sem necessidade de '?? null'
        }
    }
}



