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
            return await _userRepository.GetUserById(id);
        }

        public async Task<User> CreateUser(UserRequestDto userDto)
        {
            var user = new User
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password,
                DataNascimento = userDto.DataNascimento,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            return await _userRepository.CreateUser(userDto);
        }

        public async Task<User?> UpdateUser(User user)
        {
            var existingUser = await _userRepository.GetUserById(user.Id);
            if (existingUser == null)
                return null;

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = DateTime.Now;

            return await _userRepository.UpdateUser(existingUser);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email)
        {
            return await _userRepository.GetUserByUsernameOrEmail(username, email);
        }
    }
}
