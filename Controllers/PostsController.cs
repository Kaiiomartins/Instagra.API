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
       // Foi atualizado endpoint de imagens para poder retorna-las com id. 
        private readonly IPostService _postsService;
        private readonly IUserService _userService;

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

            var post = new Posts
            {
                Description = postDto.Conteudo,
                PostDate = DateTime.Now,
                UserId = user.Id
            };

            Posts createdPost = postDto.Imagem != null 
                ? await _postsService.CreatePostWithImagemOrImageAsync(post)
                : await _postsService.CreatePosts(post);

            return Ok(new
            {
                Conteudo = createdPost.Description,
                Imagem = createdPost.ImagemBinaria
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
        public async Task<IActionResult> UpdatePost([FromBody] PostRequestDto post)
        {
            var InfoPost = new Posts
            {
                Description = post.Conteudo,
                ImagemBinaria  = Convert.FromBase64String(post.ImagemBase64),
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
