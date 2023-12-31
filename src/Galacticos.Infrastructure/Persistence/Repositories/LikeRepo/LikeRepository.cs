using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;
using Galacticos.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Galacticos.Persistence.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApiDbContext _context;
        public LikeRepository(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Like> GetLikeByPostIdAndUserId(Guid postId, Guid userId)
        {
            return await _context.likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task<Like> LikePost(Guid postId, Guid userId)
        {
            var like = new Like
            {
                UserId = userId,
                PostId = postId
            };

            _context.likes.Add(like);
            if (await _context.SaveChangesAsync() == 0)
            {
                throw new Exception("Error saving like");
            }
            return like;
        }

        public async Task<bool> UnlikePost(Guid postId, Guid userId)
        {
            var like = await _context.likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            _context.likes.Remove(like);
            if (await _context.SaveChangesAsync() == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Like>> GetAllLikes()
        {
            return await _context.likes.ToListAsync();
        }
    }
}