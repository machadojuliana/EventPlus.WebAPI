using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IPresenca
{
    Task Cadastrar(Presenca presenca);
    Task<List<Presenca>> Listar();
    Task<List<Presenca>> ListarMinhas(Guid idUsuario);
    Task<Presenca?> BuscarId(Guid id);
    Task Atualizar(Guid id, Presenca presenca);
    Task Deletar(Guid id);
}