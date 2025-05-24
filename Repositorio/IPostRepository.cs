using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Repositorio
{
    public interface IPostRepository
    {
        Task<Posts> CreatePosts(Posts posts);

        Task<PostResponseDto> GetPostById(int id);

        Task<Posts> UpdatePostAsync(Posts posts);


        Task<Posts> DeletesPostAsync(int id);

        Task<string?> GetImagePathOrDescription(int postId);

        Task<List<Posts>> GetAllPosts(String Usernamne, DateTime? DateStart, DateTime? DateEnd);

    }
}
