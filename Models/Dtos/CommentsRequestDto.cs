using Microsoft.AspNetCore.Mvc.Formatters;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Instagram.API.Models.Dtos
{
    public class CommentsRequestDto
    {
        public int? id { get; set; }
        public DateTime DateComment
        {
            get; // => _dateStart?.ToString("dd/MM/yyyy");
            set;   //=> _dateStart = ParseDateOnly(value);
        }
        public string? TextComment { get; set; }

        [JsonIgnore]
        public DateTime? _dateStart { get; set; }

       /* private DateTime? ParseDateOnly(string value)
        {
            return DateTime.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt)
                ? dt.Date
                : null;
        }*/
        public int Userid { get; set; }

        public int PostId { get; set; }
    }
}
