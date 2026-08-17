using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace EventPlus.WebAPI.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly EventContext _context;

        public UsuarioRepository(EventContext context) 
        {
            _context = context;
        
        }
        public async Task Atualizar(Guid id, Usuario novoUsuario)
        {
            //                          buscando no banco de dados o id
            var usuarioBuscado = await _context.Usuario.FindAsync(id);

            if (usuarioBuscado != null)
            {
                usuarioBuscado.Nome = novoUsuario.Nome;
                usuarioBuscado.Email = novoUsuario.Email;
                usuarioBuscado.IdTipoUsuario = novoUsuario.IdTipoUsuario;
                if (!string.IsNullOrEmpty(novoUsuario.Senha))
                {
                    usuarioBuscado.Senha = Criptografia.GerarHash(novoUsuario.Senha);
                }

                _context.Usuario.Update(usuarioBuscado);


                await _context.SaveChangesAsync();
            }
        }

        public async Task<Usuario?> BuscarEmailSenha(string email, string senha)
        {
            var usuario = await _context.Usuario.Include(u=> u.IdTipoUsuarioNavigation).FirstAsync(u=>u.Email == email);

            if (usuario == null)
            {
                return null;
            }

            bool senhaValida = Criptografia.CompararHash(senha, usuario.Senha);

            if (!senhaValida)
            {
                return null;
            }
            return usuario;
        }

        public async Task<Usuario?> BuscarId(Guid id)
        {
            return await _context.Usuario.FirstOrDefaultAsync(t => t.IdUsuario == id);
        }

        public async Task Cadastrar(Usuario usuario)
        {
            //criptografamos a senha antes de salvar
            usuario.Senha = Criptografia.GerarHash(usuario.Senha);

            await _context.Usuario.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var usuarioBuscado = await _context.Usuario.FindAsync(id);
            if (usuarioBuscado != null)
            {
                _context.Usuario.Remove(usuarioBuscado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            //return await _context.Usuario.AsNoTracking().ToListAsync();

            //traz todas as infos de tipoUsuario
            return await _context.Usuario.Include(usuario => usuario.IdTipoUsuarioNavigation).AsNoTracking().ToListAsync();
        }

        
    }
}
