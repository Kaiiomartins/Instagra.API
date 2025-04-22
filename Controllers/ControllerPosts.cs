using Instagram.API.Data;
using Instagram.API.Models;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("post")]
    public class ControllerPosts: ControllerBase
    {

        private readonly PostsService _servicesPosts;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ControllerPosts(PostsService servicesPosts, AppDbContext context, IConfiguration configuration)
        {
            _servicesPosts = servicesPosts;
            _context = context;
            _configuration = configuration;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _servicesPosts.GetPostById(id);
            if (post == null)
            {
                Console.WriteLine($"Post com ID {id} não encontrado.");
                return null;
            }

            return Ok(post);

        }

        [HttpPost("texto")]
        public async Task<IActionResult> CreatePostText([FromBody] Posts posts)
        {
            var post = await _servicesPosts.GetPostById(posts.Id);
            if (post is null)
                return BadRequest("Já existe esse post");

            var created = await _servicesPosts.CreatePosts(posts);
            return Ok(created);
        }


        [HttpPost("imagem")]
        public async Task<IActionResult> CreatePostImagem(
            [FromForm] int userId,
            [FromForm] string description,
            [FromForm] string postType,
            [FromForm] DateTime postDate,
            [FromForm] IFormFile imagem)
        {
            var post = new Posts
            {
                UserId = userId,
                Description = description,
                PostType = postType,
                PostDate = postDate
            };

            var criado = await _servicesPosts.CreatePostComImagemAsync(post, imagem);
            return Ok(criado);
        }

        [HttpGet("imagem/{postId}")]
        public async Task<IActionResult> VisualizarImagem(int postId)
        {
            var caminhoRelativo = await _servicesPosts.GetCaminhoImagemAsync(postId);

            if (string.IsNullOrEmpty(caminhoRelativo))
                return NotFound("Imagem não encontrada.");

            var caminhoFisico = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminhoRelativo.TrimStart('/'));

            if (!System.IO.File.Exists(caminhoFisico))
                return NotFound("Arquivo não existe no disco.");

            var extensao = Path.GetExtension(caminhoFisico).ToLowerInvariant();
            var contentType = extensao switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };

            var bytes = await System.IO.File.ReadAllBytesAsync(caminhoFisico);
            return File(bytes, contentType);
        }

        [HttpPut("update")]
        public async Task<IActionResult> PutPost([FromBody] Posts posts)
        {
            var postExistente = await _servicesPosts.GetPostById(posts.Id);
            if (postExistente is null)
                return NotFound(new { mensagem = "Post não encontrado." });

            var postAtualizado = await _servicesPosts.UpdatePostAsync(posts);
            return Ok(postAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosts(int id)
        {
            var postExistente = await _servicesPosts.GetPostById(id);
            if (postExistente is null)
                return NotFound(new { mensagem = "Post não encontrado." });

            var postDeletado = await _servicesPosts.DeletesPostAsync(id);
            return Ok(postDeletado);
        }
    }
}
