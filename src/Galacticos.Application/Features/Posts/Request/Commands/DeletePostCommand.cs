using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galacticos.Application.Features.Posts.Request.Commands
{
    public class DeletePostCommand : IRequest<ErrorOr<bool>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DeletePostCommand(Guid postId, Guid userId)
        {
            PostId = postId;
            UserId = userId;
        }
    }
}
