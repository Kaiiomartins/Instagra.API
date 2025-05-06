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
            return await _context.Posts.FindAsync(id);
        }

        public async Task<List<Posts>> GetPosts()
        {
            try
            {
                return await _context.Posts.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao buscar os posts no banco de dados.", ex);
            }
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

        public async Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts, IFormFile? imagem)
        {
            if (imagem != null && imagem.Length > 0)
            {
                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(imagem.FileName)}";
                var caminho = Path.Combine("wwwroot/imagens", nomeArquivo);

                Directory.CreateDirectory(Path.GetDirectoryName(caminho)!);

                using (var stream = new FileStream(caminho, FileMode.Create))
                {
                    await imagem.CopyToAsync(stream);
                }

                posts.ImagemUrl = $"/imagens/{nomeArquivo}";
            }

            posts.PostDate = DateTime.Now;

            await _context.Posts.AddAsync(posts);
            await _context.SaveChangesAsync();

            return await _context.Posts
                .Include(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == posts.Id);
        }

        public async Task<string?> GetImagePathOrDescription(int postId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || string.IsNullOrEmpty(post.ImagemUrl))
                return null;

            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", post.ImagemUrl.TrimStart('/'));

            if (!File.Exists(caminhoFisico))
                return null;

            return post.ImagemUrl;
        }
    }
}
