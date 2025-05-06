using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    // ISP
    // DIP
    public interface IUserService : IUserRepository
    {
        Task<User?> GetUserById(int id);
        Task<User> CreateUser(UserRequestDto user);
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);
    }
}