using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class PresencaDTO
{
    [Required(ErrorMessage = "A situação da presença é obrigatória.")]
    public bool Situacao { get; set; }

    [Required(ErrorMessage = "O evento é obrigatório.")]
    public Guid IdEvento { get; set; }

    public Guid IdUsuario { get; set; }
}
