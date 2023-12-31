using Galacticos.Domain.Entities;

namespace Galacticos.Application.Persistence.Contracts;

public interface IPostTagRepository
{
    Task<PostTag> Add(PostTag postTag);
    Task<List<PostTag>> GetPostTagsByPostId(Guid postId);
    Task<List<PostTag>> GetPostTagsByTagId(Guid tagId);
    Task<PostTag> Update(Guid tagId, Guid newTagId, Guid postId);
    Task<PostTag> GetPostTagByPostIdAndTagId(Guid postId, Guid tagId);
    Task<int> Delete(PostTag postTag);
}