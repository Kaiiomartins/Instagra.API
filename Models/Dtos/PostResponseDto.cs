using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace Instagram.API.Models.Dtos
{
    public class PostResponseDto
    {
       

        public int id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime DatePublic { get; set; }
        public byte[]? ImageBytes { get; internal set; }

    }

}