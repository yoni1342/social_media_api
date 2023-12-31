using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galacticos.Application.Features.Posts.Request.Commands
{
    public class CreatePostCommand : IRequest<ErrorOr<PostResponesDTO>>
    { 
        public Guid UserId {get; set;}
        public CreatePostRequestDTO CreatePostRequestDTO {get; set;} = null!;
    }
}
