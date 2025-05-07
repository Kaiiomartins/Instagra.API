namespace Instagram.API.Models.Dtos
{
    public class PostRequestDto
    {
        public string? Titulo { get; set; }             
        public string? Conteudo { get; set; }           
        public DateTime DataPublicacao { get; set; }    

        public string? ImagemBase64 { get; set; }       
        public string? ImagemContentType { get; set; }
    }
}
