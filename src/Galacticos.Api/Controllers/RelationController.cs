using Galacticos.Application.Persistence.Contracts;
using Microsoft.AspNetCore.Mvc;
using Galacticos.Application.DTOs.Relations;
using MediatR;
using Galacticos.Application.Features.Relation.Request.Query;
using Galacticos.Application.Features.Relation.Request.Command;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Features.Profile.Request.Queries;
using System.Security.Claims;
using Galacticos.Domain.Errors;

namespace Galacticos.Api.Controllers
{
    [Route("api/[controller]")]
    public class RelationController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RelationController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Follow/{UserId}")]
        public async Task<ActionResult<Guid>> Follow(Guid UserId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var relation = new RelationDTO
                {
                    FollowerId = Guid.Parse(userIdClaim),
                    FollowedUserId = UserId
                };

                var result = await _mediator.Send(new FollowCommand { RelationDTO = relation });
                var user = await _mediator.Send(new GetProfileRequest { UserId = relation.FollowerId });

                if (result != null)
                {
                    await _mediator.Send(new CreateNotificationCommand
                    {
                        NotificationDTO = new CreateNotificationDTO
                        {
                            UserById = relation.FollowerId,
                            UserToId = relation.FollowedUserId,
                            Content = $"{user.Value.UserName} Start Following You" // Follow
                        }
                    });

                    return Ok(result);
                }

                return Guid.Empty;
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpDelete("UnFollow/{UserId}")]
        public async Task<ActionResult<Guid>> UnFollow(Guid UserId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var relation = new RelationDTO
                {
                    FollowerId = Guid.Parse(userIdClaim),
                    FollowedUserId = UserId
                };

                var result = await _mediator.Send(new UnFollowCommand { RelationDTO = relation });
                var user = await _mediator.Send(new GetProfileRequest { UserId = relation.FollowerId });

                if (result != null)
                {
                    // Send Notification
                    await _mediator.Send(new CreateNotificationCommand
                    {
                        NotificationDTO = new CreateNotificationDTO
                        {
                            UserById = relation.FollowerId,
                            UserToId = relation.FollowedUserId,
                            Content = $"{user.Value.UserName} Unfollowed You" // Unfollow
                        }
                    });

                    return Ok(result);
                }
                
                return Guid.Empty;
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("{followedId}")]
        public async Task<ActionResult<GetFollowersDTO>> Get(Guid followedId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var relation = new RelationDTO
                {
                    FollowerId = Guid.Parse(userIdClaim),
                    FollowedUserId = followedId
                };
                var result = await _mediator.Send(new GetRelationRequest { RelationDTO = relation });
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("Allfollowers")]
        public async Task<ActionResult<List<Guid>>> GetFollowersIds()
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var id = Guid.Parse(userIdClaim);
                var result = await _mediator.Send(new GetFollowersIdRequest { id = id });
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("Allfollowed")]
        public async Task<ActionResult<List<Guid>>> GetFollowedIds()
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var id = Guid.Parse(userIdClaim);
                var result = await _mediator.Send(new GetFollowedIdsRequest { id = id });
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}