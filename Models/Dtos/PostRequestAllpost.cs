using Microsoft.VisualBasic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Instagram.API.Models.Dtos
{
    public class PostRequestAllpost
    {
        public string UserName { get; set; }
        [JsonIgnore]
        public DateTime? DateStart { get; set; }

        [JsonIgnore]
        public DateTime? DateEnd { get; set; }

        [JsonPropertyName("DateStart")]
        public string DateStartFormatted
        {
            get => DateStart?.ToString("dd/MM/yyyy");
            set => DateStart = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : (DateTime?)null;
        }

        [JsonPropertyName("DateEnd")]
        public string DateEndFormatted
        {
            get => DateEnd?.ToString("dd/MM/yyyy");
            set => DateEnd = DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : (DateTime?)null;
        }
    }
}
