using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {

        private readonly IEvento _evento;
        private readonly ICloudinaryService _cloudinaryService;

        public EventoController (IEvento evento,  ICloudinaryService cloudinaryService)
        {
            _evento = evento;
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Cadastrar([FromForm] EventoDTO dto)
        {
            try
            {
                string? imagemUrl = null;

                if(dto.ArquivoImagem is not null)
                imagemUrl = await _cloudinaryService.UploadImagem(dto.ArquivoImagem);

                var evento = new Evento
                {
                    NomeEvento = dto.NomeEvento,
                    Descricao = dto.Descricao,
                    DataEvento = dto.DataEvento,
                    Urlimagem = imagemUrl, //url vinda do cloudinary (ou null)
                    IdTipoEvento = dto.IdTipoEvento,
                    IdInstituicao = dto.IdInstituicao
                    
                };

                await _evento.Cadastrar(evento);

                return StatusCode(201, evento);
                
            }
            catch(Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evento>>> Listar()
        {
            try
            {
                var eventos = await _evento.Listar();
                return Ok(eventos);
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Evento>> BuscarPorID(Guid id)
        {
            try
            {
                var evento = await _evento.BuscarPorId(id);
                if (evento == null) return NotFound("Evento não encontrado.");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] EventoDTO dto)
        {
            try
            {
                var eventoExistente = await _evento.BuscarPorId(id);
                if (eventoExistente == null) return NotFound("Evento não encontrado.");

                var evento = new Evento
                {
                    NomeEvento = dto.NomeEvento,
                    DataEvento = dto.DataEvento,
                    Descricao = dto.Descricao,
                    Urlimagem = dto.ImagemUrl,
                    IdTipoEvento = dto.IdTipoEvento,
                    IdInstituicao = dto.IdInstituicao
                };

                await _evento.Atualizar(id, evento);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var eventoExistente = await _evento.BuscarPorId(id);
                if (eventoExistente == null) return NotFound("Evento não encontrado.");

                await _evento.Deletar(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
