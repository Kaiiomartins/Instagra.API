using Microsoft.VisualBasic;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Instagram.API.Models.Dtos
{
    public class PostRequestAllpost
    {
        public string UserName { get; set; }

        [JsonPropertyName("DateStart")]
        public string? DateStart
        {
            get => _dateStart?.ToString("dd/MM/yyyy");
            set => _dateStart = ParseDateOnly(value);
        }

        [JsonPropertyName("DateEnd")]
        public string? DateEnd
        {
            get => _dateEnd?.ToString("dd/MM/yyyy");
            set => _dateEnd = ParseDateOnly(value);
        }

        [JsonIgnore]
        public DateTime? _dateStart { get; set; }

        [JsonIgnore]
        public DateTime? _dateEnd { get; set; }

        private DateTime? ParseDateOnly(string value)
        {
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                ? dt.Date
                : null;
        }
    }
}
