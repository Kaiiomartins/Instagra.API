namespace Instagram.API.Models.Dtos
{
    public class CommentsResposeDto
    {
        public int id { get; set; }

        public string? text { get; set; }

        public DateTime DateComment { get; set; }

        public DateTime DatewUpdate { get; set; }

        public int Userid { get; set; }
        public int PostId { get; set; }
    }
}
