using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{
    public interface IInstituicao
    {
        Task Cadastrar(Instituicao tipoEvento);

        Task Atualizar(Guid id, Instituicao instituicao);

        Task Deletar(Guid id);

        Task<List<Instituicao>> Listar();

        Task<Instituicao?> BuscarId(Guid id);
    }
}
