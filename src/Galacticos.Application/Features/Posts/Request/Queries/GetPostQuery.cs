using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using MediatR;

namespace Galacticos.Application.Features.Posts.Request.Queries
{
    public class GetPostQuery : IRequest<ErrorOr<PostResponesDTO>>
    {
        public Guid PostId { get; set; }

        public GetPostQuery(Guid postId)
        {
            PostId = postId;
        }
    }
}