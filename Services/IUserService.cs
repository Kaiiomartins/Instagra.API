using Instagram.API.Models.Dtos;

namespace Instagram.API.Services
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUserByUsernameOrEmail(string username, string email = null);
        Task CreateUser(UserRequestDto userDto);
        Task UpdateUser(UserRequestDto userDto);
        Task DeleteUser(string userName);
        Task<bool> Login(LoginRequestDto userDto);
    }
}