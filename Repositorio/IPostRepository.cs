using Instagram.API.Models;

namespace Instagram.API.Repositorio
{
    public interface IPostRepository
    {


        Task<Posts> CreatePosts(Posts posts);
        Task<List<Posts>> GetPosts();
        Task<Posts> GetPostById(int id);

        Task<Posts> UpdatePostAsync(Posts posts);


        Task<Posts> DeletesPostAsync(int id);

        Task<Posts> CreatePostComImagemAsync(Posts posts, IFormFile imagem);

        Task<string?> GetCaminhoImagemAsync(int postId);
    }
}
