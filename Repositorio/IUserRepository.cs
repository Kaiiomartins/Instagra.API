using Instagram.API.Models;
namespace Instagram.API.Repositorio
{
    public interface IUserRepository
    {
        Task<User?> GetUserByUsernameOrEmail(string username, string email = null);
        Task<User?> GetUserLogin(string username, string email, string password);
        Task CreateUser(User userDto );
        Task UpdateUser(User user);
        Task DeleteUser(string userName);
    }
}