
using Microsoft.EntityFrameworkCore;
using Instagram.API.Model;
using Microsoft.Identity.Client;

namespace Instagram.API.Data
{
    public class AppDbContext : DbContext 
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { 
        
        }

        public DbSet<User> Users { get; set; }  


    }
}
