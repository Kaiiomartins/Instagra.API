using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Instagram.API.Models.Dtos
{
    public class PostResponseDto
    {
       

        public int id { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataPublicacao { get; set; }

        public string? ImagemUrl { get; set; }

        public string UserName { get; set; }
        public string? ImagemBinaria { get; internal set; }

    }

}
