using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("post")]
    public class PostsController : ControllerBase
    {

        private readonly IPostService _postsService;
        private readonly IUserService _userService;

        public PostsController(IPostService postsService, IUserService userService)
        {
            _postsService = postsService;
            _userService = userService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postsService.GetPostById(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostRequestDto postDto)
        {
            var InfoUser = await _userService.GetUserByUserName(postDto.UserName);
            var post = new Posts
            {
                Description = postDto.Conteudo,
                PostDate = DateTime.Now,
                UserId = InfoUser.
            };

            Posts createdPost = imagem != null && imagem.Length > 0
                ? await _postsService.CreatePostWithImagemOrImageAsync(post, imagem)
                : await _postsService.CreatePosts(post);

            return Ok(new
            {
                Conteudo = createdPost.Description,
                Imagem = createdPost.ImagemUrl
            });

        }

        [HttpGet("imagem/{postId}")]
        public async Task<IActionResult> VisualizarImagem(int postId)
        {
            var relativePath = await _postsService.GetImagePathOrDescription(postId);
            if (string.IsNullOrWhiteSpace(relativePath))
                return NotFound();

            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = Path.GetExtension(fullPath).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream"
            };

            var imageBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            return File(imageBytes, contentType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromBody] PostRequestDto post)
        {
            var InfoPost = new Posts
            {
                Description = post.Conteudo,
                ImagemUrl = post.ImagemBase64,
                PostDate = DateTime.Now,
                UserId = post.userId
            };
            
            var updated = await _postsService.UpdatePostAsync(InfoPost);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var deleted = await _postsService.GetPostById(id);
            if (deleted == null)
                return NotFound();

            await _postsService.DeletesPostAsync(deleted.id);
            return NoContent();
        }
    }
}
