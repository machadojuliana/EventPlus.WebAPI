using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    /// <summary>
    ///  Data Tranfer Object (DTO) para cadastro e atualização do Perfil/Tipo de Usuário.
    /// </summary>
    public class TipoEventoDTO
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título pode ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;
    }
}