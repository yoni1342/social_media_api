using Galacticos.Application.Persistence.Contracts;
using Galacticos.Infrastructure.Data;
using Galacticos.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Galacticos.Infrastructure.Persistence.Repositories.PostTagRepo;

public class PostTagRepository : IPostTagRepository
{
    private readonly ApiDbContext _context;

    public PostTagRepository(ApiDbContext context)
    {
        _context = context;
    }

    public async Task<PostTag> Add(PostTag postTag)
    {
            await _context.postTags.AddAsync(postTag);
            await _context.SaveChangesAsync();
            return postTag;
    }

    public async Task<int> Delete(PostTag postTag)
    {
        var pt = await _context.postTags.Where(pt => pt.PostId == postTag.PostId && pt.TagId == postTag.TagId).FirstOrDefaultAsync();
        _context.postTags.Remove(pt);
        return await _context.SaveChangesAsync();
    }

    public Task<PostTag> GetPostTagByPostIdAndTagId(Guid postId, Guid tagId)
    {
        var postTag = _context.postTags.Where(pt => pt.PostId == postId && pt.TagId == tagId).FirstOrDefaultAsync();
        return postTag;
    }

    public async Task<List<PostTag>> GetPostTagsByPostId(Guid postId)
    {
        var postTags = await _context.postTags.Where(pt => pt.PostId == postId).ToListAsync();
        return postTags;
    }

    public async Task<List<PostTag>> GetPostTagsByTagId(Guid tagId)
    {
        var postTags = await _context.postTags.Where(pt => pt.TagId == tagId).ToListAsync();
        return postTags;
    }

    public async Task<PostTag> Update(Guid tagId, Guid newTagId, Guid postId)
    {
        var existingPostTag = await _context.postTags.Where(pt => pt.PostId == postId && pt.TagId == tagId).FirstOrDefaultAsync();
        if (existingPostTag != null)
        {
            existingPostTag.TagId = newTagId;
            await _context.SaveChangesAsync();
        }
        return existingPostTag;
    }

}