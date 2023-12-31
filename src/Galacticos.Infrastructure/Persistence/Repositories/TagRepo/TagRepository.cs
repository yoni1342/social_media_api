using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Galacticos.Infrastructure.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApiDbContext _context;

        public TagRepository(ApiDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Tag> GetById(Guid id)
        {
            return await _context.tags.FindAsync(id);
        }

        public async Task<List<Tag>> GetAll()
        {
            return await _context.tags.ToListAsync();
        }

        public async Task<Tag> Update(Tag tag)
        {
            _context.Entry(tag).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task Delete(Guid id)
        {
            var tag = await _context.tags.FindAsync(id);
            _context.tags.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.tags.AnyAsync(tag => tag.Id == id);
        }

        public async Task<List<Tag>> GetTagsByPost(Guid postId)
        {
            var tags = await _context.postTags
                .Where(pt => pt.PostId == postId)
                .Select(pt => pt.Tag)
                .ToListAsync();

            return tags;
        }

        public async Task<List<Tag>> SearchTags(string searchTerm)
        {
            var tags = await _context.tags
                .Where(tag => tag.Name.Contains(searchTerm))
                .ToListAsync();

            return tags;
        }
        public async Task<Tag> GetOrCreateTagByName(string tagName)
        {
            var existingTag = await GetTagByName(tagName);
            if (existingTag != null)
            {
                return existingTag;
            }

            var newTag = new Tag
            {
                Name = tagName
            };
            await Add(newTag);
            return newTag;
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _context.tags.SingleOrDefaultAsync(tag => tag.Name == tagName);
        }

        public async Task<Tag> Add(Tag tag)
        {
            await _context.tags.AddAsync(tag);
            await _context.SaveChangesAsync();
            return tag;
        }

        public async Task<List<Tag>> GetTagsByNames(List<string> tagNames)
        {
            List<Tag> tags = new();
            foreach (var tagName in tagNames)
            {
                var tag =  await GetTagByName(tagName);
                tags.Add(tag);
            }
            
            return tags;
        }
    }
}
