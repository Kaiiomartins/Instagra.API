using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Services
{
    public interface IPostService 
    {
        Task<IEnumerable<PostResposeAllPosts>> GetPostsAll(string username, DateTime? dateStart, DateTime? dateEnd);
        Task<Posts> CreatePosts(PostRequestDto postD);
        Task<PostResponseDto?> GetPostById(int id);
        Task<Posts?> UpdatePostAsync(Posts posts);
        Task<Posts> DeletesPostAsync(int id);
    }
}