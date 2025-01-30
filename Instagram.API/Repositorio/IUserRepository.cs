using Instagram.API.Data;
using Instagram.API.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Instagram.API.Repositorio
{
    public interface IUserRepository
    {
        Task<User?> GetById(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> Add(User user);
        Task<User> Update(User user);
        Task Delete(int id);
        Task<User?> GetUsernameOrEmail(string username, string email);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User?> GetById(int id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

      
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

       
        public async Task<User> Add(User user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        
        public async Task<User> Update(User user)
        {
            _appDbContext.Users.Update(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        
        public async Task Delete(int id)
        {
            var user = await GetById(id);
            if (user != null)
            {
                _appDbContext.Users.Remove(user);
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task <User?> GetUsernameOrEmail(string username, string email) {
           return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username || u.Email == email);
        }


    }
}