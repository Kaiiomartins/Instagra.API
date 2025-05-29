using Instagram.API.Models.Dtos;
namespace Instagram.API.Repositorio
{
    public interface ICommetsRepository
    {

        public Task <CommentsResposeDto> GetpostsAsync(int id, DateTime DateComment);

        public Task CreaatePost(CommentsRequestDto commentsRequestDto);

        public Task PutCommentsAsync(CommentsRequestDto commentsRequestDto);

        public Task DeleteCommentsAsync(int id, DateTime dateTime);
    }
}
