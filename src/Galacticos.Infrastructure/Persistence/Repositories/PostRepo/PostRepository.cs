using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galacticos.Infrastructure.Persistence.Repositories.PostRepo
{
    public class PostRepository : IPostRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;

        public PostRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<Post> Add(Post entity)
        {
            _context.posts.Add(entity);
            if(_context.SaveChanges() == 0){
                return Task.FromResult<Post>(null);
            }
            return Task.FromResult(entity);
        }

        public Task<bool> Delete(Guid id)
        {
            var post = _context.posts.FirstOrDefault(x => x.Id == id);
            if(post == null){
                return Task.FromResult(false);
            }
            _context.posts.Remove(post);
            if(_context.SaveChanges() == 0){
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<List<Post>> GetAll()
        {
            return Task.FromResult(_context.posts.Include(x => x.Comments).ToList());
        }

        public Task<Post> GetById(Guid id)
        {
            return Task.FromResult(_context.posts
                .Include(x => x.Comments)
                .Include(x => x.Likes)
                .FirstOrDefault(x => x.Id == id)!);
        }

        public Task<List<Post>> GetPostsByUserId(Guid userId)
        {
            return Task.FromResult(_context.posts.Include(x => x.Comments).Where(x => x.UserId == userId).ToList());
        }

        public Task<Post> Update(Post entity)
        {
            var postToUpdate = _context.posts.FirstOrDefault(x => x.Id == entity.Id);

            if(postToUpdate == null){
                return Task.FromResult<Post>(null);
            }

            postToUpdate.Caption = entity.Caption;
            postToUpdate.Image = entity.Image;
            postToUpdate.Likes = entity.Likes;
            postToUpdate.Comments = entity.Comments;

            if(_context.SaveChanges() == 0){
                return Task.FromResult<Post>(null);
            }

            return Task.FromResult(postToUpdate);
        }

        public Task<List<Post>> GetPostsLikedByUser(Guid userId)
        {
            var posts = _context.posts.Include(x => x.Comments).Where(x => x.Likes.Any(x => x.UserId == userId)).ToList();
            return Task.FromResult(posts);
        }

        public Task<List<Post>> GetPostsByTag(Guid tagId)
        {
            var post_ids = _context.postTags.Where(x => x.TagId == tagId).Select(x => x.PostId).ToList();
            return _context.posts.Where(x => post_ids.Contains(x.Id)).ToListAsync();
        }

        public Task<bool> Exists(Guid id)
        {
            return Task.FromResult(_context.posts.Any(x => x.Id == id));
        }
    }
}
