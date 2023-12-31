using MediatR;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.Features.Likes.Request.Queries;
using Galacticos.Application.Persistence.Contracts;

namespace Galacticos.Application.Features.Likes.Handler.Queries
{
    public class GetPostsLikedByUserRequestHandler : IRequestHandler<GetPostsLikedByUserRequest, List<PostResponesDTO>>
    {

        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public GetPostsLikedByUserRequestHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }
        public async Task<List<PostResponesDTO>> Handle(GetPostsLikedByUserRequest request, CancellationToken cancellationToken)
        {
            var posts = await _postRepository.GetPostsLikedByUser(request.UserId);
            var postDtos = _mapper.Map<List<PostResponesDTO>>(posts);
            return postDtos;
        }
    }
}