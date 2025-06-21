using Instagram.API.Data;
using Instagram.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Instagram.API.Repositorio
{
    public class PostRepository : IPostRepository
    {
        private readonly AppDbContext _context;

        public PostRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Posts>> GetAllPosts(int userId, DateTime? dateStart, DateTime? dateEnd)
        {
            var query = _context.Posts.AsQueryable();

            query = query.Where(p => p.UserId == userId);

            if (query != null)
                query = query.Where(p => p.UserId == userId);

            if (dateStart.HasValue)
                query = query.Where(p => p.CreatedAt.Date >= dateStart.Value && p.IsDeleted == false);

            if (dateEnd.HasValue)
                query = query.Where(p => p.CreatedAt.Date <= dateEnd.Value && p.IsDeleted == false);

            return await query.ToListAsync();
        }
        public async Task<Posts?> GetPostById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
        }
        public async Task<Posts> CreatePosts(Posts posts)
        {
            await _context.Posts.AddAsync(posts);
            await _context.SaveChangesAsync();
            return posts;
        }
        public async Task<Posts?> UpdatePostAsync(Posts posts)
        {
            var post = await _context.Posts.FindAsync(posts.Id);

            if (post == null)
                return null;

            _context.Entry(post).CurrentValues.SetValues(posts);
            await _context.SaveChangesAsync();
        
            return posts;
        }

        public async Task<Posts?> DeletesPostAsync(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
                return null;

            post.IsDeleted = true;
            post.IsDeletedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return post;
        }

    }
}
