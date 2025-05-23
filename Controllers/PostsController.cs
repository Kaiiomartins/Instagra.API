using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

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
                Description = postDto.Description,
                PostDate = DateTime.Now,
                UserId = user.Id,
                ImageBytes = imagemBytes
            };

            var createdPost = await _postsService.CreatePosts(post);

            return Ok(new
            {
                Description = createdPost.Description,
                Imagem = createdPost.ImageBytes != null && contentType != null
                    ? $"data:{contentType};base64,{Convert.ToBase64String(createdPost.ImageBytes)}"
                    : null,
                UserID = user.UserName
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostComImagem(int id)
        {
            var result = await _postsService.Getpostwithimage(id);

            if (result == null)
                return NotFound();

            return Ok(result);

        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] PostRequestDto post)
        {
            byte[] imagemBytes;

            var User = _userService.GetUserByUsernameOrEmail(post.UserName);

            using (var memoryStream = new MemoryStream())
            {
                await post.Imagem.CopyToAsync(memoryStream);
                imagemBytes = memoryStream.ToArray();
            }

            var infoPost = new Posts
            {
                Description = post.Description,
                ImageBytes = imagemBytes,
                PostDate = DateTime.Now,
                UserId = User.Id,
                Id = User.Id,
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

        [HttpGet("All")]
        public async Task<ActionResult<List<PostResposeAllPosts>>> GetAll([FromBody] PostResposeAllPosts postsInfo)
        {
            var user = await _userService.GetUserByUsernameOrEmail(postsInfo.UserName);
            var posts = await _postsService.GetPostsAll(postsInfo.UserName, postsInfo.DateStart, postsInfo.DateEnd);

            var response = posts.Select(post => new PostResposeAllPosts
            {
                UserName = user.UserName,
                Description = post.Description,
                DateStart = post.PostDate,
                DateEnd = post.PostDate
            }).ToList();

            return Ok(response);
        }
    }
}
