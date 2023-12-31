using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using MediatR;

namespace Galacticos.Application.Features.Comments.Request.Commands
{
    public class CreateCommentCommand : IRequest<ErrorOr<CommentResponesDTO>>
    {
        public string Content { get; set; } = null!;
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}