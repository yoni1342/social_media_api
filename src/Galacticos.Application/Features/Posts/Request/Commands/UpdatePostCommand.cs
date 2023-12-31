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
    public class UpdatePostCommand : IRequest<ErrorOr<PostResponesDTO>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public required UpdatePostRequestDTO UpdatePostRequestDTO { get; set; }
    }
}
