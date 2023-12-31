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
    public class GetPostsLikedByUserQueryHandler : IRequestHandler<GetPostsLikedByUserQuery, ErrorOr<List<PostResponesDTO>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetPostsLikedByUserQueryHandler(IPostRepository postRepository, IMapper mapper, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<PostResponesDTO>>> Handle(GetPostsLikedByUserQuery request, CancellationToken cancellationToken)
        {
            User user = _userRepository.GetUserById(request.UserId);

            if (user == null)
            {
                return new ErrorOr<List<PostResponesDTO>>().Errors;

            }

            List<Post> posts = await _postRepository.GetPostsLikedByUser(request.UserId);
            posts = posts.OrderByDescending(x => x.CreatedAt).ToList();
            List<PostResponesDTO> postResponses = _mapper.Map<List<PostResponesDTO>>(posts);

            return postResponses;
        }
    }
}