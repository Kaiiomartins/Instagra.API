using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Services
{
    public interface IPostService 
    {
        Task<Posts> CreatePosts(Posts posts);

        Task<PostResponseDto?> GetPostById(int id);

        Task<Posts?> UpdatePostAsync(Posts posts);

        Task<Posts> DeletesPostAsync(int id);

        Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts);

        Task<string?> GetImagePathOrDescription(int postId);

        Task<object?> Getpostwithimage(int id);

        Task<List<Posts>> GetPostsAll(String Usernamne, DateTime? DateStart, DateTime? DateEnd);

    }
}