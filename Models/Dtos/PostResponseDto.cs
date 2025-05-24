namespace Instagram.API.Models.Dtos
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ImageBase64 { get; internal set; }
    }
}