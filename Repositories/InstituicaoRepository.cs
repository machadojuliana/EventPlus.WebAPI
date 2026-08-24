using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories
{
    public class InstituicaoRepository : IInstituicao
    {
        private readonly EventContext _context;

        public InstituicaoRepository(EventContext context)
        {
            _context = context;
        }

        public async Task Atualizar(Guid id, Instituicao novaInstituicao)
        {
            var instituicaoBuscado = await _context.Instituicao.FindAsync(id);



            if (instituicaoBuscado != null)
            {
                instituicaoBuscado.Cnpj = novaInstituicao.Cnpj;
                instituicaoBuscado.NomeFantasia = novaInstituicao.NomeFantasia;
                instituicaoBuscado.Endereco = novaInstituicao.Endereco;

                _context.Instituicao.Update(instituicaoBuscado);


                await _context.SaveChangesAsync();
            }
        }

        public async Task<Instituicao?> BuscarId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Cadastrar(Instituicao instituicao)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Instituicao>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
