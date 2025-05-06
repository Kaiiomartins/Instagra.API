using Instagram.API.Data;
using Instagram.API.Models;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController: ControllerBase
    {

        private readonly PostsService _servicesPosts;
        
        private readonly IConfiguration _configuration;

        public PostsController(PostsService servicesPosts, IConfiguration configuration)
        {
            _servicesPosts = servicesPosts;
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreatePost(
            [FromForm] int userId,
            [FromForm] string description,
            [FromForm] string postType,
            [FromForm] DateTime? postDate,
            [FromForm] IFormFile? imagem)
        {
            var post = new Posts
            {
                UserId = userId,
                Description = description,
                PostType = postType,
                PostDate = postDate ?? DateTime.Now
            };

            var existente = await _servicesPosts.GetPostById(post.Id);
            if (existente != null)
                return BadRequest("Já existe esse post");

            if (imagem != null && imagem.Length > 0)
            {
                var criado = await _servicesPosts.CreatePostWithImagemOrImageAsync(post, imagem);
                return Ok(criado);
            }
            else
            {
                var criado = await _servicesPosts.CreatePosts(post);
                return Ok(criado);
            }
        }
        [HttpGet("imagem/{postId}")]
        public async Task<IActionResult> VisualizarImagem(int postId)
        {
            var caminhoRelativo = await _servicesPosts.GetImagePathOrDescription(postId);

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
