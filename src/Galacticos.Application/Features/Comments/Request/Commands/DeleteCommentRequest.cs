using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using MediatR;

namespace Galacticos.Application.Features.Comments.Request.Commands
{
    public class DeleteCommentRequest : IRequest<ErrorOr<bool>>
    {
        public Guid Id { get; set; }
        public Guid UserId {get; set;}
    }
}