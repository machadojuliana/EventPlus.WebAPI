using EventPlus.WebAPI.DTO;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
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
    }
}
