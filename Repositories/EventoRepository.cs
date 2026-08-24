using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class EventoRepository : IEvento
{
    private readonly EventContext _context;
    public EventoRepository(EventContext context)
    {
        _context = context;
    }

    public Task Atualizar(Guid id, Evento evento)
    {
        throw new NotImplementedException();
    }

    public Task<Evento?> BuscarPorId(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Cadastrar(Evento evento)
    {
        await _context.Evento.AddAsync(evento);
        await _context.SaveChangesAsync();
    }

    public Task Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Evento>> Listar()
    {
        throw new NotImplementedException();
    }

    public Task<List<Evento>> ListarProximos()
    {
        throw new NotImplementedException();
    }
}

