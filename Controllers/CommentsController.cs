using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;
using Instagram.API.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.API.Controllers
{
    [ApiController]
    [Route("comments")]
    public class CommentsController : Controller
    {
        private readonly CommentsService _commentsService;
        public CommentsController(CommentsService commentsService)
        {
            _commentsService = commentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments([FromBody] CommentsRequestDto comment)
        {
            var comments = await _commentsService.GetCommentsAsync(comment);
            if (comments == null)
                return NotFound(new { mensagem = "Comentário não encontrado." });
            return Ok(comments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentsRequestDto Comments)
        {
            var comment = await _commentsService.GetCommentsAsync(Comments);
            if (comment == null)
                return NotFound(new { mensagem = "Not Found" });

            var newComment = await _commentsService.CreateCommentsAsync(Comments);
            var viewModel = new CommentsResposeDto
            {
                id = newComment.id,
                text = newComment.text,
                DateComment = newComment.DateComment,
                DatewUpdate = newComment.DatewUpdate
            };

            return Ok(viewModel);
        }
        public async Task<IActionResult> UpdateComment([FromBody] CommentsRequestDto comment)
        {
            var existingComment = await _commentsService.GetCommentsAsync(comment);
            if (existingComment == null)
                return NotFound(new { mensagem = "Comentário não encontrado." });
            await _commentsService.UpdateCommentsAsync(comment);
            return NoContent();
        }
        public async Task<IActionResult> DeleteComment([FromBody] CommentsRequestDto ComentsDelete)
        {
            var existingComment = await _commentsService.GetCommentsAsync(ComentsDelete);
            if (existingComment == null)
                return NotFound(new { mensagem = "Comentário não encontrado." });
            await _commentsService.DeleteCommentsAsync(ComentsDelete);
            return NoContent();

        }
    }
}
