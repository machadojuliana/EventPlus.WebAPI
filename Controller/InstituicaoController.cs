using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicao _instituicao;

        public InstituicaoController(IInstituicao instituicao)
        {
            _instituicao = instituicao;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarId(Guid id)
        {
            try
            {
                var instituicaoBuscada = await _instituicao.BuscarId(id);

                if (instituicaoBuscada == null)
                {
                    return NotFound("Instituição não encontrada.");
                }

                return Ok(instituicaoBuscada);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _instituicao.Listar();

                return Ok(tipos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] InstituicaoDTO dto)
        {
            try
            {
                var instituicao = new Instituicao
                {
                    Cnpj = dto.Cnpj,
                    NomeFantasia = dto.NomeFantasia,
                    Endereco = dto.Endereco
                };

                await _instituicao.Cadastrar(instituicao);
                return Ok(instituicao);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] InstituicaoDTO dto)
        {
            try
            {
                var instituicao = new Instituicao
                {
                    Cnpj = dto.Cnpj,
                    NomeFantasia = dto.NomeFantasia,
                    Endereco = dto.Endereco
                };


                await _instituicao.Atualizar(id, instituicao);

                return Ok(instituicao);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(Guid id)
        {
            try
            {
                await _instituicao.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
