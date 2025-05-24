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

        public async Task<bool> Login(LoginRequestDto userDto)
        {
            var user = await _userRepository.GetUserLogin(userDto.UserName, userDto.Email, userDto.Password);
            return user != null;
        }

        public async Task<UserResponseDto?> GetUserByUsernameOrEmail(string username, string email = null)
        {
            var user = await _userRepository.GetUserByUsernameOrEmail(username, email);
            if (user == null)
                return null;    

            return  new UserResponseDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                BirthData = user.BirthDate,
                Cpf = user.Cpf
            };
        }

        public async Task CreateUser(UserRequestDto userDto)
        {
            var user = User.Create(userDto);
            await _userRepository.CreateUser(user);
        }

        public async Task UpdateUser(UserRequestDto userDto)
        {
            var userExistente = await _userRepository.GetUserByUsernameOrEmail(userDto.UserName);
            if (userExistente == null)
                throw new Exception("Usuário não encontrado!");

            userExistente.Cpf = userDto.Cpf;
            userExistente.UserName = userDto.UserName;
            userExistente.Email = userDto.Email;
            userExistente.BirthDate = userDto.BirthDate;
            userExistente.UpdatedAt = DateTime.Now;

            await _userRepository.UpdateUser(userExistente);
        }

        public async Task DeleteUser(string userName)
        {
            await _userRepository.DeleteUser(userName);
        }
    }
}
