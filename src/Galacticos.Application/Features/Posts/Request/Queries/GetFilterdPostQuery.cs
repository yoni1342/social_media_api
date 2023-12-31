using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using MediatR;

namespace Galacticos.Application.Features.Posts.Request.Queries
{
    public class GetFilterdPostQuery : IRequest<List<PostResponesDTO>>
    {
        public List<string> Tag { get; set; }

        public GetFilterdPostQuery(List<string> tag)
        {
            Tag = tag;
        }
    }
}