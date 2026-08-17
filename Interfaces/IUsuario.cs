using EventPlus.WebAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.Interfaces
{
    public interface IUsuario
    {
        Task Cadastrar(Usuario usuario);

        Task Atualizar(Guid id, Usuario novoUsuario);

        Task Deletar (Guid id);

        Task<List<Usuario>> Listar();

        Task<Usuario?> BuscarId(Guid id);

        Task<Usuario?> BuscarEmailSenha(string email, string senha);

    }
}
