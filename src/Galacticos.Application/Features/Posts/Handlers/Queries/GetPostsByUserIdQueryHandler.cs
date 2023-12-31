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
using MediatR;

namespace Galacticos.Application.Features.Posts.Handlers.Queries
{
    public class GetPostsByUserIdQueryHandler : IRequestHandler<GetPostsByUserIdQuery, ErrorOr<List<PostResponesDTO>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public GetPostsByUserIdQueryHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<ErrorOr<List<PostResponesDTO>>> Handle(GetPostsByUserIdQuery request, CancellationToken cancellationToken)
        {
            List<Post> posts = await _postRepository.GetPostsByUserId(request.UserId);
            if (posts == null)
            {
                return new ErrorOr<List<PostResponesDTO>>().Errors;
            }

            var response = _mapper.Map<List<PostResponesDTO>>(posts);
            return response;
        }
    }
}