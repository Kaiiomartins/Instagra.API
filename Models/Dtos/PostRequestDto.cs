using System.ComponentModel.DataAnnotations;

namespace Instagram.API.Models.Dtos
{
    public class PostRequestDto
    {
        public string? Description { get; set; }           
        public string UserName { get; set; }
        public IFormFile Image { get; set; }
        [Required]
        public string PostType { get; set;  }
    }
}
