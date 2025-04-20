using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Instagram.API.Models
{

    [Table ("Users")]
    public class User 
    {
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

