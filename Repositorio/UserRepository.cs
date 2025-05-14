using Instagram.API.Data;
using Instagram.API.Models;
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

        public async Task<User?> GetUserByUsernameOrEmail(string username, string email = null)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username || u.Email == email);
        }

        public async Task<User?> GetUserLogin(string username, string email, string password)
        {
            return await _context.Users
                .SingleOrDefaultAsync(u => (u.UserName == username || u.Email == email) && u.Password == password);
        }

        public async Task CreateUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
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
    }
}
