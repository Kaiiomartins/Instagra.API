using Instagram.API.Models.Dtos;
namespace Instagram.API.Services
{
    public interface ICommentsService
    {
        public Task<CommentsResposeDto> GetCommentsAsync(CommentsRequestDto comment);

        public Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment);

        public Task DeleteCommentsAsync(CommentsRequestDto Data); 

        public Task UpdateCommentsAsync(CommentsRequestDto comment);
    }
}
