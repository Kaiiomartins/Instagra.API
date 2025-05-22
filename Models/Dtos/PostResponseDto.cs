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
        public byte? ImageBinaria { get; internal set; }

    }

}
