using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Services
{
    // ISP
    // DIP
    public interface IUserService
    {
        Task<User?> GetUserById(int id);
        Task<User> CreateUser(UserRequestDto user);
        Task<User> Update(User user);
        Task DeleteUser(int id);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);
    }
}