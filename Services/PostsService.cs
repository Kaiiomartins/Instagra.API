using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;
using System.Drawing;

namespace Instagram.API.Services
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostsService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Posts> CreatePosts(Posts posts)
        {
            return await _postRepository.CreatePosts(posts);
        }

        public async Task<PostResponseDto?> GetPostById(int id)
        {
            return await _postRepository.GetPostById(id);
        }

        public async Task<Posts?> UpdatePostAsync(Posts posts)
        {
            return await _postRepository.UpdatePostAsync(posts);
        }

        public async Task<Posts?> DeletesPostAsync(int id)
        {
            return await _postRepository.DeletesPostAsync(id);
        }

        public async Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts)
        {
       
                return await _postRepository.CreatePostWithImagemOrImageAsync(posts);
            
        }

        public async Task<string?> GetImagePathOrDescription(int postId)
        {
            return await _postRepository.GetImagePathOrDescription(postId);
        }
        public async Task<object?> GetPostComImagemBase64(int id)
        {
            var post = await GetPostById(id);
            if (post == null)
                return null;

            var relativePath = await GetImagePathOrDescription(id);
            string? imagemBase64 = null;

            if (!string.IsNullOrWhiteSpace(relativePath))
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));

                if (File.Exists(fullPath))
                {
                    var imageBytes = await File.ReadAllBytesAsync(fullPath);

                    var contentType = Path.GetExtension(fullPath).ToLower() switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        _ => "application/octet-stream"
                    };

                    imagemBase64 = $"data:{contentType};base64,{Convert.ToBase64String(imageBytes)}";
                }
            }

            return new
            {
                Post = post,
                ImagemBase64 = imagemBase64
            };
        }


    }
}
