using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Models
{
    [Table ("Users")]
    public class User 
    {
        #region Properties
        [Key]
        public  int Id { get; set; }
        
        [StringLength(11)]
        public string cpf { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }


        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Email { get; set;  }

        public DateTime? DataNascimento { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public   DateTime ? UpdatedAt { get; set; } = DateTime.Now;

        #endregion

        public static User Create(UserRequestDto userDto)
        {
            return new User
            {
                cpf = userDto.Cpf,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                DataNascimento = userDto.DataNascimento,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };
        }

        public string GetDataNascimentoFormatted()
        {
            return DataNascimento?.ToString("dd/MM/yyyy") ?? string.Empty;
        }

        public string GetCreatedAtFormatted()
        {
            return CreatedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
        }

        public string GetUpdatedAtFormatted()
        {
            return UpdatedAt?.ToString("dd/MM/yyyy") ?? string.Empty;
        }
    }

}

