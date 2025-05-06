using Instagram.API.Data;
using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Instagram.API.Repositorio
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserRequestDto?> GetUserByUserName(string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            return new UserRequestDto
            {
                Cpf = user.cpf,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                DataNascimento = (DateTime)user.DataNascimento
            };
        }

        public async Task<UserRequestDto> CreateUser(UserRequestDto userDto)
        {
            var user = new User
            {
                cpf = userDto.Cpf,
                UserName = userDto.UserName,
                Password = userDto.Password,
                Email = userDto.Email,
                DataNascimento = userDto.DataNascimento,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var result = new UserRequestDto
            {
               
                Cpf = user.cpf,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                DataNascimento = (DateTime)user.DataNascimento
            };

            return result;
        }

        public async Task DeleteUser(string userName)
        {
            var user = await _context.Users.FindAsync(userName);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username || u.Email == email);
        }

        public async Task<UserRequestDto> UpdateUser(UserRequestDto userDto)
        {
            var userExistente = await _context.Users.FindAsync(userDto.UserName);
            if (userExistente == null)
                throw new Exception("Usuário não encontrado, parceiro!");

            userExistente.cpf = userDto.Cpf;
            userExistente.UserName = userDto.UserName;
            userExistente.Password = userDto.Password;
            userExistente.Email = userDto.Email;
            userExistente.DataNascimento = userDto.DataNascimento;
            userExistente.UpdatedAt = DateTime.Now;

            _context.Users.Update(userExistente);
            await _context.SaveChangesAsync();

            return userDto;
        }
    }
}
