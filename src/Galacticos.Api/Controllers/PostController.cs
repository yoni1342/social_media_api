using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Posts;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Features.Posts.Request.Commands;
using Galacticos.Application.Features.Posts.Request.Queries;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.Features.Auth.Requests.Commands;
using Galacticos.Application.Features.Relation.Request.Query;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Galacticos.Application.Features.Profile.Request.Queries;


namespace Galacticos.Api.Controllers
{

    [Route("api/[controller]")]
    public class PostController : ApiController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public PostController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequestDTO request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var command = new CreatePostCommand()
                {
                    UserId = Guid.Parse(userIdClaim),
                    CreatePostRequestDTO = request
                };
                ErrorOr<PostResponesDTO> result = await _mediator.Send(command);

                var follower_ids = await _mediator.Send(new GetFollowersIdRequest() { id = Guid.Parse(userIdClaim) });
                var user = await _mediator.Send(new GetProfileRequest() { UserId = Guid.Parse(userIdClaim) }); 

                foreach (var follower_id in follower_ids)
                {
                    await _mediator.Send(new CreateNotificationCommand()
                    {
                        NotificationDTO = new CreateNotificationDTO()
                        {
                            UserToId = follower_id.Id,
                            UserById = Guid.Parse(userIdClaim),
                            Content = $"{user.Value.UserName} has posted a new post"
                        }
                    });
                }

                return result.Match<IActionResult>(
                    post => Ok(post),
                    errors => Problem(errors)
                );
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            var query = new GetPostQuery(postId);
            ErrorOr<PostResponesDTO> result = await _mediator.Send(query);

            return result.Match<IActionResult>(
                post => Ok(post),
                errors => Problem(errors)
            );
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostsByUserId(Guid userId)
        {
            var query = new GetPostsByUserIdQuery(userId);
            ErrorOr<List<PostResponesDTO>> result = await _mediator.Send(query);

            return result.Match<IActionResult>(
                posts => Ok(posts),
                errors => Problem(errors)
            );
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(Guid postId,[FromForm] UpdatePostRequestDTO updatePostRequestDTO)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            UpdatePostCommand request = new UpdatePostCommand()
            {
                PostId = postId,
                UserId = Guid.Parse(userIdClaim),
                UpdatePostRequestDTO = updatePostRequestDTO
            };
            ErrorOr<PostResponesDTO> result = await _mediator.Send(request);

            return result.Match<IActionResult>(
                post => Ok(post),
                errors => Problem(errors)
            );
        }

        [HttpGet("user/{userId}/liked")]
        public async Task<IActionResult> GetPostsLikedByUser(Guid userId)
        {
            var query = new GetPostsLikedByUserQuery(userId);
            ErrorOr<List<PostResponesDTO>> result = await _mediator.Send(query);

            return result.Match<IActionResult>(
                posts => Ok(posts),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var command = new DeletePostCommand(postId, Guid.Parse(userIdClaim));
            ErrorOr<bool> result = await _mediator.Send(command);

            return result.Match<IActionResult>(
                post => Ok(post),
                errors => Problem(errors)
            );
        }

        [HttpPost("filter")]
        public async Task<IActionResult> FilterPostByTag(List<string> tags)
        {
            var query = new GetFilterdPostQuery(tags);
            List<PostResponesDTO> result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
