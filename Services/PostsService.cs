using Microsoft.EntityFrameworkCore;
using Instagram.API.Data;
using Instagram.API.Models;

namespace Instagram.API.Services
{
    public class PostsService : Metodos
    {
        private readonly AppDbContext _appContext;



        public PostsService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<Posts> CreatePosts(Posts posts)
        {
            await _appContext.Posts.AddAsync(posts);
            await _appContext.SaveChangesAsync();
            return posts;
        }

        public async Task<Posts?> GetPostById(int id)
        {
           
            var post = await _appContext.Posts.FindAsync(id);

            if (post == null)
                return null;

            
            return post;

        }

        public async Task<List<Posts>> GetPosts()
        {
            try
            {
                return await _appContext.Posts.ToListAsync();
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("Erro ao buscar os posts no banco de dados.", ex);
            }

            _appContext.SaveChanges();
        }


        public async Task<Posts?> UpdatePostAsync(Posts posts)
        {
            var post = await _appContext.Posts.FindAsync(posts.Id);

            if (post == null)
                return null;

            _appContext.Entry(post).CurrentValues.SetValues(posts);
            await _appContext.SaveChangesAsync();

            return posts;
        }

        public async Task<Posts> DeletesPostAsync(int id)
        {

            var Post = await _appContext.Posts.FindAsync(id);

            if (Post == null)
                return null;

            _appContext.Posts.Remove(Post);

            await _appContext.SaveChangesAsync();

            return Post;


        }

        public async Task<Posts> CreatePostComImagemAsync(Posts posts, IFormFile imagem)
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

            await _appContext.Posts.AddAsync(posts);
            await _appContext.SaveChangesAsync();

            return posts;
        }

        public async Task<string?> GetCaminhoImagemAsync(int postId)
        {
            var post = await _appContext.Posts.FindAsync(postId);

            if (post == null || string.IsNullOrEmpty(post.ImagemUrl))
                return null;

            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", post.ImagemUrl.TrimStart('/'));

            Console.WriteLine($" Caminho gerado: {caminhoFisico}");

            if (!File.Exists(caminhoFisico))
            {
                Console.WriteLine(" Arquivo não encontrado.");
                return null;
            }

            Console.WriteLine(" Arquivo encontrado com sucesso.");
            return post.ImagemUrl;
        }




        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _appContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user == null)
                return null;

            return new User
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                DataNascimento = user.DataNascimento,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt.GetValueOrDefault()

                // Não precisa carregar Posts aqui a menos que você queira incluir também.
            };
        }

    }
}
