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

        [HttpPost("All")]
        public async Task<ActionResult<IEnumerable<PostResposeAllPosts>>> GetAll([FromBody] PostRequestAllpost postsInfo)
        {
            if (!string.IsNullOrEmpty(postsInfo.DateStartFormatted))
                postsInfo.DateStart = DateTime.Parse(postsInfo.DateStartFormatted);
            if (!string.IsNullOrEmpty(postsInfo.DateEndFormatted))
                postsInfo.DateEnd = DateTime.Parse(postsInfo.DateEndFormatted);

            var response = await _postsService.GetPostsAll(postsInfo.UserName, postsInfo.DateStart, postsInfo.DateEnd);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var result = await _postsService.GetPostById(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] PostRequestDto postDto)
        {
            var createdPost = await _postsService.CreatePosts(postDto);
            return Ok(createdPost);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePost([FromForm] PostRequestDto post)
        {
            byte[] imagemBytes;

            var User = _userService.GetUserByUsernameOrEmail(post.UserName);

            using (var memoryStream = new MemoryStream())
            {
                await post.Image.CopyToAsync(memoryStream);
                imagemBytes = memoryStream.ToArray();
            }

            var infoPost = new Posts
            {
                Description = post.Description,
                ImageBytes = imagemBytes,
                Date = DateTime.Now,
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

            await _postsService.DeletesPostAsync(deleted.Id);
            return NoContent();
        }
    }
}
