using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Persistence.Contracts
{
    public interface ICommentRepository
    {
        Task<Comment> CreateComment(Comment comment);
        Task<Comment>? GetCommentById(Guid id);
        Task<List<Comment>> GetCommentsByPostId(Guid postId);
        Task<Comment> UpdateComment(Comment comment);
        Task<bool> DeleteComment(Guid id);   
    }
}