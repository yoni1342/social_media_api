using AutoMapper;
using MediatR;
using Galacticos.Application.Features.NewsFeed.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.DTOs.Posts;
// using Galacticos.Application.DTOs.Newsfeed;

namespace Galacticos.Application.Features.NewsFeed.Handler.Queries
{
    public class GetNewsFeedPostsRequestHandler : IRequestHandler<GetNewsFeedPostsRequest, List<PostResponesDTO>>
    {
        private readonly IMapper _mapper;
        private readonly INewsFeedRepository _newsFeedRepository;
        public GetNewsFeedPostsRequestHandler(IMapper mapper, INewsFeedRepository newsFeedRepository)
        {
            _mapper = mapper;
            _newsFeedRepository = newsFeedRepository;
        }
        public async Task<List<PostResponesDTO>> Handle(GetNewsFeedPostsRequest request, CancellationToken cancellationToken)
        {
            var newsFeedPosts = await _newsFeedRepository.GetNewsFeedForUser(request.Id, request.PageNumber, request.PageSize);
            return _mapper.Map<List<PostResponesDTO>>(newsFeedPosts);
        }   
    }
}