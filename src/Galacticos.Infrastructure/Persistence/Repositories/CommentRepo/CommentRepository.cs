using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;
using Galacticos.Domain.Errors;
using AutoMapper;

namespace Galacticos.Infrastructure.Persistence.Repositories.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApiDbContext _context;
        private IMapper _mapper;
        public CommentRepository(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Task<Comment> CreateComment(Comment comment)
        {
            _context.comments.Add(comment);
            if(_context.SaveChanges() == 0)
            {
                return Task.FromResult<Comment>(null);
            }
            return Task.FromResult(comment);
        }

        public Task<bool> DeleteComment(Guid id)
        {
            var commentToDelete =_context.comments.FirstOrDefault(x=>x.Id == id);
            if (commentToDelete == null)
            {
                return Task.FromResult(false);
            }
            _context.comments.Remove(commentToDelete);
            if (_context.SaveChanges() == 0)
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }

        public Task<Comment>? GetCommentById(Guid id)
        {
            return Task.FromResult(_context.comments.FirstOrDefault(x => x.Id == id));
        }

        public Task<List<Comment>> GetCommentsByPostId(Guid postId)
        {
            var post = _context.posts.FirstOrDefault(x => x.Id == postId);
            if(post == null)
            {
                throw new Exception("Post not found");
            }
            return Task.FromResult(_context.comments.Where(x => x.PostId == postId).ToList());
        }

        public Task<Comment> UpdateComment(Comment comment)
        {
            Comment commentToEdit = _context.comments.FirstOrDefault(c=>c.Id == comment.Id);
            if(commentToEdit == null)
            {
                throw new Exception("User not found");
            }
            commentToEdit.Content = comment.Content;
            comment.UpdatedAt = DateTime.UtcNow;

            if(_context.SaveChanges() == 0)
            {
                throw new Exception("Comment not edited");
            }

            return Task.FromResult(comment);
        }

    }
}