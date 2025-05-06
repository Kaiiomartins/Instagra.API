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

        public async Task<UserRequestDto?> GetUserByUserName(string UserName)
        {
            return await _userRepository.GetUserByUserName(UserName);
        }

        public async Task<UserRequestDto> CreateUser(UserRequestDto userDto)
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

        public async Task<UserRequestDto?> UpdateUser(UserRequestDto user)
        {
            var existingUser = await _userRepository.GetUserByUserName(user.UserName);
            if (existingUser == null)
                return null;

            
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.DataNascimento = user.DataNascimento;
            existingUser.Password = user.Password;
            
            

            return await _userRepository.UpdateUser(existingUser);
        }

        public async Task DeleteUser(string UserName)
        {
            await _userRepository.DeleteUser(UserName);
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email)
        {
            return await _userRepository.GetUserByUsernameOrEmail(username, email);
        }
    }
}
