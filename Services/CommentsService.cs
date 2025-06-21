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

        public async Task<CommentsResposeDto> GetCommentsAsync(CommentsRequestDto comments)
        {
            var commment = await _repository.GetpostsAsync((int)comments.id);
            
            var commentss = new CommentsResposeDto
            {
                id = (int)commment.Id,
                text = comments.TextComment,
                DateComment = commment.DateComment ?? DateTime.Now,
                DateUpdate = DateTime.UtcNow,
                Userid = commment.UserId,  
                PostId = commment.PostId
            };

            return commentss;
        }

        public async Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment)
        {
            var newComment = new Comments
            {
                Comment = comment.TextComment,
                DateComment = DateTime.Now,
                DateUpdated = DateTime.Now,
                IsDeleted = false,
                UserId = comment.Userid,
                PostId = comment.PostId
            };

            await _repository.CreateComments(newComment);



            var viewModel = new CommentsResposeDto
            {
                id = newComment.Id,
                text = newComment.Comment,
                DateComment = DateTime.Now,
                DateUpdate = DateTime.Now,
                Userid = newComment.UserId,
                PostId = newComment.PostId
            };

            return viewModel;
         }

        public async Task UpdateCommentsAsync(CommentsRequestDto comment)
        {
            var comments = await _repository.GetpostsAsync((int)comment.id);
            if (comments == null)
                throw new Exception("Comment not found");

            var updatedComment = new Comments
            {
                Id = (long)comment.id,
                Comment = comment.TextComment,
                DateUpdated = DateTime.Now,
                IsDeleted = false,
                UserId = comment.Userid,
                PostId = comment.PostId
            };

            await _repository.PutCommentsAsync(updatedComment);
        }

        public async Task DeleteCommentsAsync(CommentsRequestDto ComentsDelete)
        {
            
            var commentId = ComentsDelete.id;

            var comments = await _repository.GetpostsAsync((int)commentId);
            if (comments == null)
                throw new Exception("Comment not found");

            await _repository.DeleteCommentsAsync((int)commentId);
        }
    }
}
