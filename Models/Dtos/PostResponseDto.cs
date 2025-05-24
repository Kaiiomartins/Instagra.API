namespace Instagram.API.Models.Dtos
{
    public class PostResponseDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DatePublic { get; set; }
        public byte[]? ImageBytes { get; internal set; }
    }
}