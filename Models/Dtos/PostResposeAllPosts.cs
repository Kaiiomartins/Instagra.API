﻿namespace Instagram.API.Models.Dtos
{
    public class PostResposeAllPosts
    {
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Description { get; set; }
        public string ImageBase64 { get; set; }
    }
}
