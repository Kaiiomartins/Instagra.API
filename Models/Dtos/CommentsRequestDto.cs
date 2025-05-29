using Microsoft.AspNetCore.Mvc.Formatters;

namespace Instagram.API.Models.Dtos
{
    public class CommentsRequestDto
    {
        public int id { get; set; }
        public DateTime DateComment { get; set; }

        public string? TextComment { get; set; }

    }
}
