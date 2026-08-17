using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{

    public class TipoEventoRepository : ITipoEvento
    {
        private readonly EventContext _context;

        public TipoEventoRepository(EventContext context)
        {
            _context = context;
        }

        //                 guid id: id do objt buscado
        //                 novoTipo: objt com novas infos
        // TipoEvento: Classe
        // novoTipo: Objt dessa classe
        // Titulo: propriedade do objt
        public async Task Atualizar(Guid id, TipoEvento novoTipo)
        {
            // variavel q guarda resultado da busca (o objt que queremos trocar pelo novoTipo
            var tipoBuscado = await _context.TipoEvento.FindAsync(id);
                            // null ou objt encontrado

            // se o tipoBuscado existir
            if (tipoBuscado != null)
            {
                // Han Jisung      = novo valor é Hannie
                tipoBuscado.Titulo = novoTipo.Titulo;
                //substituir o titulo do objt buscado pelo titulo do novoTipo

                await _context.SaveChangesAsync();
            }
        }

        public async Task<TipoEvento?> BuscarId(Guid id)
        {
            return await _context.TipoEvento.FirstOrDefaultAsync(tipo => tipo.IdTipoEvento == id);
        }

        public async Task Cadastrar(TipoEvento tipoEvento)
        {
           await _context.TipoEvento.AddAsync(tipoEvento);
            await _context.SaveChangesAsync();
        }

        public async Task Deletar(Guid id)
        {
            var tipoBuscado = await _context.TipoEvento.FindAsync(id);

            if (tipoBuscado != null)
            {
                _context.TipoEvento.Remove(tipoBuscado);

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TipoEvento>> Listar()
        {
            return await _context.TipoEvento.AsNoTracking().ToListAsync();
        }
    }
}
