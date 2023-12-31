using Galacticos.Domain.Entities;


namespace Galacticos.Application.Persistence.Contracts
{
    public interface ILikeRepository
    {
        Task<Like> LikePost(Guid postId, Guid userId);
        Task<Like> GetLikeByPostIdAndUserId(Guid postId, Guid userId);
        Task<bool> UnlikePost(Guid postId, Guid userId);

        Task<List<Like>> GetAllLikes();

    }
}