using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ComentarioController : ControllerBase
{
    private readonly IModerationService _moderationService;
    private readonly IComentario _comentario;

    public ComentarioController(IComentario comentarioRepository, IModerationService moderationService)
    {
        _comentario = comentarioRepository;
        _moderationService = moderationService;
    }

    // Retorna absolutamente todos os comentários
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comentario>>> Listar()
    {
        var comentarios = await _comentario.Listar();
        return Ok(comentarios);
    }

    // Retorna apenas comentários públicos (Exibe = true)
    [HttpGet("Evento/{idEvento:guid}")]
    public async Task<ActionResult<IEnumerable<Comentario>>> ListarPorEvento(Guid idEvento)
    {
        var comentarios = await _comentario.ListarPorEvento(idEvento);

        if (comentarios == null || !comentarios.Any())
        {
            return NotFound("Nenhum comentário público foi encontrado para este evento.");
        }

        return Ok(comentarios);
    }

    

    [HttpPost]
    public async Task<ActionResult<Comentario>> Cadastrar([FromBody] ComentarioDTO dto)
    {
        try
        {
            bool reprovado = await _moderationService.ModerarTexto(dto.Descricao);


            var comentario = new Comentario
            {
                Descricao = dto.Descricao,
                IdEvento = dto.IdEvento,
                IdUsuario = dto.IdUsuario,
                Exibe = !reprovado
            };



            await _comentario.Cadastrar(comentario);

            return StatusCode(201, comentario);

        }
        catch (Exception error)
        {
            return BadRequest(error.Message);
        }
    }



    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Deletar(Guid id)
    {
        try
        {
            var comentarioExistente = await _comentario.BuscarPorId(id);

            if (comentarioExistente == null)
            {
                return NotFound("Comentário não encontrado.");
            }

            await _comentario.Deletar(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Comentario>> BuscarPorId(Guid id)
    {
        var comentario = await _comentario.BuscarPorId(id);

        if (comentario == null)
        {
            return NotFound("Comentário não encontrado.");
        }

        return Ok(comentario);
    }

}