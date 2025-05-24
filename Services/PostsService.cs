using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    public class PostsService: IPostService 
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
            var post = await _postRepository.GetPostById(id);
            if (post == null)
                return null;

            return new PostResponseDto
            {

                id = post.id,
                Description = post.Description,
                DatePublic = post.DatePublic,
                ImageBytes = post.ImageBytes
            }; 
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
        public async Task<object?> Getpostwithimage(int id)
        {
            var post = await GetPostById(id);
            if (post == null)
                return null;

            var relativePath = await GetImagePathOrDescription(id);
            string? imagemBase64 = null;

            if (!string.IsNullOrWhiteSpace(relativePath))
            {

                if (File.Exists(relativePath))
                {
                    var imageBytes = await File.ReadAllBytesAsync(relativePath);

                    var contentType = Path.GetExtension(relativePath).ToLower() switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        _ => "application/octet-stream"
                    };

                    imagemBase64 = $"data:{contentType};base64,{Convert.ToBase64String(imageBytes)}";
                }
            }
            return post = new PostResponseDto
            {
                Description = post.Description,
                ImageBytes = post.ImageBytes,
            };
        }
           public async Task<List<Posts>> GetPostsAll(string Usernamne, DateTime? DateStart, DateTime? DateEnd) {
            var posts = await _postRepository.GetAllPosts(Usernamne, DateStart, DateEnd);
            return posts;
        }
    }
}

