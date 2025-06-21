using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromBody] CommentsRequestDto comment)
        {
            var comments = await _commentsService.GetCommentsAsync(comment);
            if (comments == null)
                return NotFound();
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentsRequestDto Comments) 
        {
         
            var newComment = await _commentsService.CreateCommentsAsync(Comments);
            return Ok(newComment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentsRequestDto comment)
        {
            var existingComment = await _commentsService.GetCommentsAsync(comment);
            if (existingComment == null)
                return NotFound();
            await _commentsService.UpdateCommentsAsync(comment);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteComment([FromBody] CommentsRequestDto ComentsDelete)
        {
            var existingComment = await _commentsService.GetCommentsAsync(ComentsDelete);
            if (existingComment == null)
                return NotFound();
            await _commentsService.DeleteCommentsAsync(ComentsDelete);
            return NoContent();
        }
    }
}
