using Instagram.API.Data;
using Instagram.API.Models;
using Instagram.API.Models.Dtos;
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

        public async Task<PostResponseDto?> GetPostById(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null) return null;

            string? imagemBase64 = null;
            string? contentType = null;

            if (post.ImagemBinaria != null && post.ImagemBinaria.Length > 0)
            {
                
                if (System.IO.File.Exists(imagemBase64))
                {
                    var extensao = Path.GetExtension(imagemBase64.ToLowerInvariant());
                    contentType = extensao switch
                    {
                        ".jpg" or ".jpeg" => "image/jpeg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".webp" => "image/webp",
                        _ => "application/octet-stream"
                    };

                    var bytes = await System.IO.File.ReadAllBytesAsync(imagemBase64);
                    imagemBase64 = Convert.ToBase64String(bytes);
                }
            }

            return new PostResponseDto
            {
                Conteudo = post.Description ?? string.Empty,
                DataPublicacao = post.PostDate,
                ImagemBinaria = Convert.ToBase64String(post.ImagemBinaria)
            };
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

        public async Task<Posts> CreatePostWithImagemOrImageAsync(Posts posts)
        {
            if (posts != null && posts.Length > 0)
            { 

                return null; 
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
            if (post == null || post.ImagemBinaria == null || post.ImagemBinaria.Length == 0)
                return null;

            return Convert.ToBase64String(post.ImagemBinaria);
        }
    }
}
