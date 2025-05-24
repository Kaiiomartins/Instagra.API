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
        public int Id { get; set; }
        
        [StringLength(11)]
        public string Cpf { get; set; }

        [Required]
        [StringLength(255)]
        public string UserName { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string Email { get; set;  }

        [Required]
        public DateTime BirthDate { get; set; }

        public DateTime? CreatedAt { get; set; }

        public   DateTime? UpdatedAt { get; set; }

        #endregion

        public static User Create(UserRequestDto userDto)
        {
            return new User
            {
                Cpf = userDto.Cpf,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };
        }
    }
}

