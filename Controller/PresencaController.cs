using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PresencaController : ControllerBase
{
    private readonly IPresenca _presencaRepository;

    public PresencaController(IPresenca presencaRepository) => _presencaRepository = presencaRepository;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Presenca>>> Listar()
    {
        return Ok(await _presencaRepository.Listar());
    }

    [HttpGet("Minhas/{idUsuario:guid}")]
    public async Task<ActionResult<IEnumerable<Presenca>>> ListarMinhas(Guid idUsuario)
    {
        return Ok(await _presencaRepository.ListarMinhas(idUsuario));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Presenca>> BuscarPorId(Guid id)
    {
        var presenca = await _presencaRepository.BuscarId(id);
        return presenca == null ? NotFound("Presença não encontrada.") : Ok(presenca);
    }

    [HttpPost]
    public async Task<ActionResult<Presenca>> Cadastrar([FromBody] PresencaDTO dto)
    {
        var presenca = new Presenca { Situacao = dto.Situacao, IdEvento = dto.IdEvento, IdUsuario = dto.IdUsuario };
        await _presencaRepository.Cadastrar(presenca);
        return CreatedAtAction(nameof(BuscarPorId), new { id = presenca.IdPresenca }, presenca);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Atualizar(Guid id, [FromBody] PresencaDTO dto)
    {
        var presencaExistente = await _presencaRepository.BuscarId(id);
        if (presencaExistente == null) return NotFound("Presença não encontrada.");

        await _presencaRepository.Atualizar(id, new Presenca { Situacao = dto.Situacao });
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var presencaExistente = await _presencaRepository.BuscarId(id);
        if (presencaExistente == null) return NotFound("Presença não encontrada.");

        await _presencaRepository.Deletar(id);
        return NoContent();
    }
}
