using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Repositorio
{
    public interface IUserRepository 
    {
        Task<UserRequestDto?> GetUserByUserName(string UserName);
        Task<UserRequestDto> CreateUser(UserRequestDto userDto );
        Task<UserRequestDto> UpdateUser(UserRequestDto user);
        Task DeleteUser(string userName);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);
    }


}