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
        private byte[] imagemBytes;

        public PostsController(IPostService postsService, IUserService userService)
        {
            _postsService = postsService;
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostRequestDto postDto)
        {
            var user = await _userService.GetUserByUsernameOrEmail(postDto.UserName);
            if (user == null)
                return NotFound("Usuário não encontrado!");

            byte[]? imagemBytes = null;
            string? contentType = null;

            if (postDto.Imagem != null && postDto.Imagem.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await postDto.Imagem.CopyToAsync(memoryStream);
                    imagemBytes = memoryStream.ToArray();
                    contentType = postDto.Imagem.ContentType;
                }
            }

            var post = new Posts
            {
                Description = postDto.Conteudo,
                PostDate = DateTime.Now,
                UserId = user.Id,
                ImageBinario = imagemBytes
            };

            var createdPost = await _postsService.CreatePosts(post);

            return Ok(new
            {
                Conteudo = createdPost.Description,
                Imagem = createdPost.ImageBinario != null && contentType != null
                    ? $"data:{contentType};base64,{Convert.ToBase64String(createdPost.ImageBinario)}"
                    : null
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostComImagem(int id)
        {
            var resultado = await _postsService.GetPostComImagemBase64(id);

            if (resultado == null)
                return NotFound();

            return Ok(resultado);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] PostRequestDto post)
        {
            byte[] imagemBytes;

            using (var memoryStream = new MemoryStream())
            {
                await post.Imagem.CopyToAsync(memoryStream);
                imagemBytes = memoryStream.ToArray();
            }

            var infoPost = new Posts
            {
                Description = post.Conteudo,
                ImageBinario = imagemBytes,
                PostDate = DateTime.Now,
                UserId = post.userId,
                Id = post.Id,
            };

            var updated = await _postsService.UpdatePostAsync(infoPost);
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
