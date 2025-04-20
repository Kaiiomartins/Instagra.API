using Microsoft.EntityFrameworkCore;
using Instagram.API.Models;

namespace Instagram.API.Data
{
    public class AppDbContext : DbContext 
    {

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { 
        
        }

        public DbSet<User> Users { get; set; }


        public DbSet<Posts> Posts { get; set; }
        public DbSet<Follows> Followers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Follows>()
                .HasKey(f => new { f.SeguidorId, f.SeguidoId });


            modelBuilder.Entity<Follows>()
                .HasOne(f => f.Seguidor)
                .WithMany()
                .HasForeignKey(f => f.SeguidorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follows>()
                .HasOne(f => f.Seguido)
                .WithMany()
                .HasForeignKey(f => f.SeguidoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
