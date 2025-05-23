using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System.Text.Json.Serialization;

namespace Instagram.API.Models.Dtos
{
    public class PostResposeAllPosts
    {

        public string UserName { get; set; }

        [JsonIgnore]
        public DateTime? DateStart { get; set; }
        [JsonIgnore]
        public DateTime? DateEnd { get; set; }

        public Byte[]? Image { get; set; }

        public string Description { get; set; }

        public string DateStartFormatted => DateStart?.ToString("dd/MM/yyyy");
        public string DateEndFormatted => DateEnd?.ToString("dd/MM/yyyy");


    }
}
