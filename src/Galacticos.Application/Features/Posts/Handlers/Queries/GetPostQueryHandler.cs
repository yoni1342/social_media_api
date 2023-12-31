using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.Features.Posts.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Posts.Handlers.Queries
{
    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, ErrorOr<PostResponesDTO>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        
        public GetPostQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<ErrorOr<PostResponesDTO>> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            Post post = await _postRepository.GetById(request.PostId);
            if (post == null)
            {
                return Errors.Post.PostNotFound;
            }

            var response = _mapper.Map<PostResponesDTO>(post);
            return response;
        }
    }
}