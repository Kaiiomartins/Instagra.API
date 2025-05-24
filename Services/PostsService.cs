using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    public class PostsService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserService _userService;

        public PostsService(
            IPostRepository postRepository,
            IUserService userService
        )
        {
            _postRepository = postRepository;
            _userService = userService;
        }

        public async Task<PostResponseDto?> GetPostById(int id)
        {
            var post = await _postRepository.GetPostById(id);
            if (post == null)
                return null;

            return new PostResponseDto
            {
                Id = post.Id,
                Description = post.Description,
                CreatedAt = post.PostDate,
                ImageBase64 = post.ImageBytes != null ? Convert.ToBase64String(post.ImageBytes) : null
            };
        }

        public async Task<Posts> CreatePosts(PostRequestDto postDto)
        {
            var user = await _userService.GetUserByUsernameOrEmail(postDto.UserName);
            if (user == null)
                throw new Exception("Usuário não encontrado ou não existe!");

            byte[]? imagemBytes = null;
            string? contentType = null;

            if (postDto.Image != null && postDto.Image.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await postDto.Image.CopyToAsync(memoryStream);
                    imagemBytes = memoryStream.ToArray();
                    contentType = postDto.Image.ContentType;
                }
            }

            var post = new Posts
            {
                Description = postDto.Description,
                PostDate = DateTime.Now,
                UserId = user.Id,
                PostType = postDto.PostType,
                ImageBytes = imagemBytes
            };

            return await _postRepository.CreatePosts(post);
        }

        public async Task<Posts?> UpdatePostAsync(Posts posts)
        {
            return await _postRepository.UpdatePostAsync(posts);
        }

        public async Task<Posts?> DeletesPostAsync(int id)
        {
            return await _postRepository.DeletesPostAsync(id);
        }

        public async Task<List<Posts>> GetPostsAll(string Usernamne, DateTime? DateStart, DateTime? DateEnd)
        {
            var posts = await _postRepository.GetAllPosts(Usernamne, DateStart, DateEnd);
            return posts;
        }
    }
}

