using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controller
{
    [Route("api/[controller]")] //htttp://localhost:5170/api/TipoEvento
    [ApiController]
    public class TipoEventoController : ControllerBase
    {
        private readonly ITipoEvento _tipoEvento;
        public TipoEventoController(ITipoEvento tipoEvento)
        {
            _tipoEvento = tipoEvento;
        }

        // /api/TipoEvento/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarId(Guid id)
        {
            try
            {
                var tipoEventoBuscado = await _tipoEvento.BuscarId(id);

                if (tipoEventoBuscado == null)
                {
                    return NotFound("Tipo de evento não encontrado.");
                }

                return Ok(tipoEventoBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] TipoEventoDTO dto)
        {
            try
            {
                var tipoEvento = new TipoEvento {
                    Titulo = dto.Titulo
                };

                await _tipoEvento.Cadastrar(tipoEvento);

                return StatusCode(201, tipoEvento);
            }
            catch (Exception error) 
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try // oq esperamos q de certo
            {
                var tipos = await _tipoEvento.Listar();
                return Ok(tipos);
            }
            catch (Exception error) // se der errado nao vai quebrar o codigo, segue com um tratamento diferente
            {
                return BadRequest(error.Message);
                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] TipoEventoDTO dto)
        {
            try
            {
                //cria novo objt e atribui valores do objt passado (dto)
                var tipoEvento = new TipoEvento
                {
                    Titulo = dto.Titulo
                };

                //chama o metodo atualizar e passa id e o objt
                await _tipoEvento.Atualizar(id, tipoEvento);

                return NoContent();
            }
            catch (Exception error) 
            {
                return BadRequest(error);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _tipoEvento.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
