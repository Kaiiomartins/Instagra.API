using System.ComponentModel.DataAnnotations;

namespace Instagram.API.Models.Dtos
{
    public class UserRequestDto
    {
        [Required]
        [StringLength(11, ErrorMessage = "O CPF deve ter no máximo 11 caracteres.")]
        public string Cpf { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "O nome de usuário deve ter no máximo 255 caracteres.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres.")]
        public string Password { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "O e-mail deve ter no máximo 255 caracteres.")]
        [EmailAddress(ErrorMessage = "O e-mail fornecido não é válido.")]
        public string Email { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }
    }
}
