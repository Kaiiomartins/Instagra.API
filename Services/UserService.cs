using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _userRepository.GetById(id);
        }
        
        public async Task<User> CreateUser(UserRequestDto userDto)
        {
            var user = User.Create(userDto);
            return await _userRepository.Add(user);
        }

        public async Task<User> Update(User user)
        {
            return await _userRepository.Update(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.Delete(id);
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email)
        {
            return await _userRepository.GetUsernameOrEmail(username, email);
        }
    }
}
