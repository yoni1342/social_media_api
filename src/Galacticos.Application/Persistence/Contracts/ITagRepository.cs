using Galacticos.Domain.Entities;


namespace Galacticos.Application.Persistence.Contracts;
public interface ITagRepository
{
    Task<Tag> GetById(Guid id);
    Task<List<Tag>> GetAll();
    Task<List<Tag>> GetTagsByPost(Guid postId);
    Task<Tag> Add(Tag tag);
    Task<Tag> Update(Tag tag);
    Task Delete(Guid id);
    Task<bool> Exists(Guid id);
    Task<List<Tag>> SearchTags(string searchTerm);
    Task<Tag> GetOrCreateTagByName(string hashtag);
    Task<Tag> GetTagByName(string tagName);
    Task<List<Tag>> GetTagsByNames(List<string> tagNames);
}
