using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    public interface IPostService : IPostRepository
    {
        Task<Posts> CreatePosts(Posts posts);

        Task<PostResponseDto?> GetPostById(int id);

        Task<Posts?> UpdatePostAsync(Posts posts);

        Task<Posts> DeletesPostAsync(int id);

        Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts, IFormFile imagem);

        Task<string?> GetImagePathOrDescription(int postId);

    }
}