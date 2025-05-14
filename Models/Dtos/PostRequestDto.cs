namespace Instagram.API.Models.Dtos
{
    public class PostRequestDto
    {
        public int userId { get; set; }
        public string? Titulo { get; set; }             
        public string? Conteudo { get; set; }           
        public DateTime DataPublicacao { get; set; }    
        public string? ImagemBase64 { get; set; }       
        public string? ImagemContentType { get; set; }
        public string UserName { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
