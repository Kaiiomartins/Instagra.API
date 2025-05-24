using Instagram.API.Models;

namespace Instagram.API.Repositorio
{
    public interface IPostRepository
    {
        Task<Posts> CreatePosts(Posts posts);

        Task<Posts> GetPostById(int id);

        Task<Posts> UpdatePostAsync(Posts posts);

        Task<Posts> DeletesPostAsync(int id);

        Task<List<Posts>> GetAllPosts(String Usernamne, DateTime? DateStart, DateTime? DateEnd);
    }
}
