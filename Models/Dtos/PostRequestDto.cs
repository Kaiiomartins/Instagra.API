namespace Instagram.API.Models.Dtos
{
    public class PostRequestDto
    {
        public string? Description { get; set; }           
        public string UserName { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
