using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using MediatR;

namespace Galacticos.Application.Features.Comments.Request.Commands
{
    public class UpdateCommentCommand : IRequest<ErrorOr<CommentResponesDTO>>
    {
        public Guid CommentId { get; set; }

        public Guid UserId {get; set;}
        public string Content { get; set; }
        
    }
}