namespace Instagram.API.Models.Dtos
{
    public class PostResponseDto
    {
       
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataPublicacao { get; set; }

        public string? ImagemUrl { get; set; }

     
    }

}
