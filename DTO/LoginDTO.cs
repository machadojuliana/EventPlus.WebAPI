using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email é obrigatorio")]
        [EmailAddress(ErrorMessage = "informe um email valido")]

        public string Email { get; set; }

        [Required(ErrorMessage = "a senha é obrigatoria para autenticacao")]
        [StringLength(60, MinimumLength= 8, ErrorMessage = "a senha deve ter entre 8 a 60 caracteres")]
        public string Senha { get; set; } = string.Empty;
       
    }
}
