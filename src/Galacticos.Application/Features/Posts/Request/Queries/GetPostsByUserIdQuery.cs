using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using MediatR;

namespace Galacticos.Application.Features.Posts.Request.Queries
{
    public class GetPostsByUserIdQuery : IRequest<ErrorOr<List<PostResponesDTO>>>
    {
        public Guid UserId { get; set; }

        public GetPostsByUserIdQuery(Guid userId)
        {
            UserId = userId;
        }
    }
}