using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventPlus.WebAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuario;
        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarId(Guid id)
        {
            try
            {
                var usuarioBuscado = await _usuario.BuscarId(id);

                if (usuarioBuscado == null)
                {
                    return NotFound("usuário não encontrado.");
                }

                return Ok(usuarioBuscado);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar([FromBody] UsuarioDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    IdTipoUsuario = dto.IdTipoUsuario
                };

                await _usuario.Cadastrar(usuario);
                return Ok(usuario);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var tipos = await _usuario.Listar();

                return Ok(tipos);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] UsuarioDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    Senha = dto.Senha,
                    IdTipoUsuario = dto.IdTipoUsuario
                };


                await _usuario.Atualizar(id, usuario);

                return Ok(usuario);
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
                await _usuario.Deletar(id);

                return NoContent();
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}
