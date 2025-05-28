using System.ComponentModel.DataAnnotations;

namespace Instagram.API.Models.Dtos
{
    public class PostRequestDto
    {
        public required string UserName { get; set; }
        public required string PostType { get; set;  }
        public string? Description { get; set; }           
        public IFormFile? Image { get; set; }
    }
}
