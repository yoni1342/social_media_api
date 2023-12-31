using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;
using Galacticos.Application.Features.Likes.Command.Queries;
using ErrorOr;
using Galacticos.Application.DTOs.Likes;
using Galacticos.Application.Features.Posts.Request.Queries;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.Features.Profile.Request.Queries;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Api.Controllers
{
    [Route("api/[controller]")]
    public class LikeController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LikeController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("{PostId}")]
        public async Task<IActionResult> LikePost(Guid PostId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var command = new LikePostRequest { PostId = PostId, UserId = Guid.Parse(userIdClaim) };
            ErrorOr<LikeResponseDto> result = await _mediator.Send(command);

            var post = await _mediator.Send(new GetPostQuery(PostId));
            var user = await _mediator.Send(new GetProfileRequest { UserId = Guid.Parse(userIdClaim) });
            if (post.Value != null)
            {
                await _mediator.Send(new CreateNotificationCommand
                {
                    NotificationDTO = new CreateNotificationDTO
                    {
                        UserById = command.UserId,
                        UserToId = post.Value.UserId,
                        Content = $"{user.Value.UserName} Liked Your Post" // Like Post
                    }
                });
            }

            return result.Match<IActionResult>(
                like => Ok(like),
                errors => Problem(errors)
            );

        }

        [HttpDelete("{PostId}")]
        public async Task<IActionResult> UnlikePost(Guid PostId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var command = new DislikePostRequest { PostId = PostId, UserId = Guid.Parse(userIdClaim) };
            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match<IActionResult>(
                like => Ok(like),
                errors => Problem(errors)
            );
        }
    }
}