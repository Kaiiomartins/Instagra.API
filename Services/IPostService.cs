using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Services
{
    public interface IPostService 
    {
        Task<Posts> CreatePosts(PostRequestDto postD);
        Task<PostResponseDto?> GetPostById(int id);
        Task<Posts?> UpdatePostAsync(Posts posts);
        Task<Posts> DeletesPostAsync(int id);
        Task<List<Posts>> GetPostsAll(String Usernamne, DateTime? DateStart, DateTime? DateEnd);
    }
}