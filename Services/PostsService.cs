using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

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

        public async Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts, IFormFile imagem)
        {
            return await _postRepository.CreatePostWithImagemOrImageAsync(posts, imagem);
        }

        public async Task<string?> GetImagePathOrDescription(int postId)
        {
            return await _postRepository.GetImagePathOrDescription(postId);
        }
    }
}
