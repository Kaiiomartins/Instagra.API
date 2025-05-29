using Instagram.API.Models;
using Instagram.API.Models.Dtos;
using Microsoft.Identity.Client;
namespace Instagram.API.Services
{
    public interface ICommentsService
    {

        public Task<CommentsResposeDto> GetCommentsAsync(CommentsRequestDto comment); 

        public Task<CommentsResposeDto> CreateCommentsAsync(CommentsRequestDto comment);

        public Task DeleteCommentsAsync(int Id);

        public Task UpdateCommentsAsync(CommentsRequestDto comment);
    }
}
