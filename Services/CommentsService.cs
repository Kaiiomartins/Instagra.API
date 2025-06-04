using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Instagram.API.Repositorio;

namespace Instagram.API.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly CommetsRespository _repository;

        public CommentsService(CommetsRespository repository)
        {
            _repository = repository;
        }

        public async Task<CommentsResposeDto> GetCommentsAsync(CommentsRequestDto comment)
        {
            if (!DateTime.TryParse(comment.DateComment.ToString(), out var parsedDateComment))
                throw new Exception("Invalid date format for DateComment");

            var commment = await _repository.GetpostsAsync(comment.id, parsedDateComment);
            if (commment == null)
                throw new Exception("Comment not found");

            var comments = new CommentsResposeDto
            {
                id = comment.id,
                text = comment.TextComment, 
                DateComment = parsedDateComment,
                DatewUpdate = DateTime.UtcNow
            };

            return comments;
        }

        public async Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment)
        {
            if (!DateTime.TryParse(comment.DateComment.ToString(), out var parsedDateComment))
                throw new Exception("Invalid date format for DateComment");

            var newComment = new Comments
            {
                Id = comment.id,
                Comment = comment.TextComment,
                DateComment = parsedDateComment,
                DateUpdated = DateTime.UtcNow,
                IsDeleted = false,
                UserId = comment.Userid,
                PostId = comment.PostId
            };

            await _repository.CreateComments(newComment);

            var viewModel = new CommentsResposeDto
            {
                id = (int)newComment.Id, 
                text = newComment.Comment,
                DateComment = parsedDateComment,
                DatewUpdate = DateTime.UtcNow,
                Userid = newComment.UserId,
                PostId = newComment.PostId
            };

            return viewModel;
        }

        public async Task UpdateCommentsAsync(CommentsRequestDto comment)
        {
            if (!DateTime.TryParse(comment.DateComment.ToString(), out var parsedDateComment))
                throw new Exception("Invalid date format for DateComment");

            var comments = await _repository.GetpostsAsync(comment.id, parsedDateComment);
            if (comments == null)
                throw new Exception("Comment not found");

            await _repository.PutCommentsAsync(comment);
        }

        public async Task DeleteCommentsAsync(CommentsRequestDto ComentsDelete)
        {
            if (!DateTime.TryParse(ComentsDelete.DateComment.ToString(), out var parsedDateComment))
                throw new Exception("Invalid date format for DateComment");

            var comments = await _repository.GetpostsAsync(ComentsDelete.id, parsedDateComment);
            if (comments == null)
                throw new Exception("Comment not found");

            await _repository.DeleteCommentsAsync(ComentsDelete.id, parsedDateComment);
        }
    }
}
