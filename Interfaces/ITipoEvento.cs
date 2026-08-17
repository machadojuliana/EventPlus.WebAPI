using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces
{

    // interface do repositorio para TipoEvento
    // contrato do TipoEvento metodos que devem ser implementados dentro do repositorio

    public interface ITipoEvento
    {
        Task Cadastrar(TipoEvento tipoEvento);

        Task Atualizar(Guid id, TipoEvento tipoEvento);

        Task Deletar(Guid id);

        Task<List<TipoEvento>> Listar();

        Task<TipoEvento?> BuscarId(Guid id);
    }
}
