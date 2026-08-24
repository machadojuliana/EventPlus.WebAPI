using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO
{
    public class InstituicaoDTO
    {
        // CNPJ
        [Required(ErrorMessage = "O CNPJ é obrigatório.")]
        [StringLength(14, ErrorMessage = "O CNPJ pode ter no máximo 14 caracteres.")]
        public string Cnpj { get; set; } = string.Empty;

        // NOME FANTASIA
        [Required(ErrorMessage = "O nome fantasia é obrigatorio")]
        [StringLength(100, ErrorMessage = "O nome deve ter no maximo 100 caracteres")]
        public string NomeFantasia { get; set; } = string.Empty;

        // Endereco
        [Required(ErrorMessage = "O endereco é obrigatorio")]
        [StringLength(100, ErrorMessage = "O endereco deve ter no maximo 100 caracteres")]
        public string Endereco { get; set; } = string.Empty;
    }
}