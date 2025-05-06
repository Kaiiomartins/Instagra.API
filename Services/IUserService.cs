using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    // ISP
    // DIP
    public interface IUserService : IUserRepository
    {
        Task<UserRequestDto?> GetUserByUserName(string UserName);
        Task<UserRequestDto> CreateUser(UserRequestDto user);
        Task<UserRequestDto> UpdateUser(UserRequestDto user);
        Task DeleteUser(string userName);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);
    }
}