using ErrorOr;
using Galacticos.Application.DTOs.Likes;
using MediatR;


namespace Galacticos.Application.Features.Likes.Command.Queries
{
    public class LikePostRequest : IRequest<ErrorOr<LikeResponseDto>>
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
    }
}