using Instagram.API.Models;
using Instagram.API.Models.Dtos;

namespace Instagram.API.Repositorio
{
    public interface IUserRepository 
    {
        Task<User?> GetUserById(int id);
        Task<User> CreateUser(UserRequestDto userDto );
        Task<User> UpdateUser(User user);
        Task DeleteUser(int id);
        Task<User?> GetUserByUsernameOrEmail(string username, string email);
    }


}