using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using MediatR;

namespace Galacticos.Application.Features.Comments.Request.Queries
{
    public class GetCommentByIdRequest : IRequest<ErrorOr<CommentResponesDTO>>
    {
        public Guid Id { get; set; }  
    }
}