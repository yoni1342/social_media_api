using Galacticos.Application.DTOs.Posts;
using MediatR;

namespace Galacticos.Application.Features.NewsFeed.Request.Queries
{
    public class GetNewsFeedPostsRequest : IRequest<List<PostResponesDTO>>
    {
        public Guid Id { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}