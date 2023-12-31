using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using MediatR;

namespace Galacticos.Application.Features.Comments.Request.Queries
{
    public class GetCommentsByPostIdRequest : IRequest<ErrorOr<List<CommentResponesDTO>>>
    {
        public Guid PostId { get; set; }
    }
}