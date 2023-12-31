using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Comments;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Features.Comments.Request.Commands;
using Galacticos.Application.Features.Comments.Request.Queries;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.Features.Posts.Request.Queries;
using Galacticos.Application.Features.Profile.Request.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Galacticos.Api.Controllers
{
    [Route("api/comment")]
    public class CommentController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(Guid PostId, [FromBody] CreateCommentRequestDTO request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            var command = _mapper.Map<CreateCommentCommand>(request);
            command.PostId = PostId;
            command.UserId = Guid.Parse(userIdClaim);
            ErrorOr<CommentResponesDTO> res = await _mediator.Send(command);

            var post = await _mediator.Send(new GetPostQuery(PostId));

            var user = await _mediator.Send(new GetProfileRequest { UserId = command.UserId });


            if (post.Value != null)
            {
                await _mediator.Send(new CreateNotificationCommand
                {
                    NotificationDTO = new CreateNotificationDTO
                    {
                        UserById = command.UserId,
                        UserToId = post.Value.UserId,
                        Content = $"{user.Value.UserName} Commented On Your Post"
                    }
                });
            }

            return res.Match<IActionResult>(
                comment => Ok(comment),
                errors => Problem(errors)
            );
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCommentById(Guid Id)
        {
          ErrorOr<CommentResponesDTO> res = await _mediator.Send(new GetCommentByIdRequest { Id = Id });

            return res.Match<IActionResult>(
                comment => Ok(comment),
                errors => Problem(errors)
            );
        }
        [HttpGet("post/{PostId}")]
        public async Task<IActionResult> GetCommentsByPostId(Guid PostId)
        {
            ErrorOr<List<CommentResponesDTO>> res = await _mediator.Send(new GetCommentsByPostIdRequest { PostId = PostId });

            Console.WriteLine("CommentController: " + res.ToString());
            return res.Match<IActionResult>(
                comments => Ok(comments),
                errors => Problem(errors)
            );
        }

        [HttpPut("{CommentId}")]
        public async Task<IActionResult> UpdateComment(Guid CommentId, [FromBody] UpdateCommentRequestDTO request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var command = _mapper.Map<UpdateCommentCommand>(request);
            command.CommentId = CommentId;
            command.UserId = Guid.Parse(userIdClaim!);
            ErrorOr<CommentResponesDTO> res = await _mediator.Send(command);

            return res.Match<IActionResult>(
                comment => Ok(comment),
                errors => Problem(errors)
            );
        }

        [HttpDelete("post/{Id}")]
        public async Task<IActionResult> DeleteComment(Guid Id)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");
            
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var command = new DeleteCommentRequest { Id = Id, UserId = Guid.Parse(userIdClaim) };
            ErrorOr<bool> res = await _mediator.Send(command);

            return res.Match<IActionResult>(
                comment => Ok(comment),
                errors => Problem(errors)
            );
        }
    }
}
