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

        public async Task<Posts> CreatePosts(Posts posts)
        {
            await _context.Posts.AddAsync(posts);
            await _context.SaveChangesAsync();
            return posts;
        }

        public async Task<Posts?> GetPostById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            return post;
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

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<List<Posts>> GetAllPosts(string userName, DateTime? dateStart, DateTime? dateEnd)
        {
            var usuario = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (usuario == null)
                return new List<Posts>();

            var query = _context.Posts.AsQueryable();

            query = query.Where(p => p.UserId == usuario.Id);

            if (dateStart.HasValue)
                query = query.Where(p => p.PostDate.Date >= dateStart.Value);

            if (dateEnd.HasValue)
                query = query.Where(p => p.PostDate.Date <= dateEnd.Value);

            return await query.ToListAsync();
        }

    }
}
