using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Security.Claims;
using Galacticos.Application.Features.NewsFeed.Request.Queries;
using Galacticos.Application.DTOs.Posts;
using AutoMapper;

namespace Galacticos.Api.Controllers
{
    [Route("api/[controller]")]
    public class NewsFeedController : ApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NewsFeedController(IMediator mediator, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("posts/{pageNumber}/{pageSize}")]
        public async Task<ActionResult<List<PostResponesDTO>>> GetNewsFeedPosts(int pageNumber, int pageSize)
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");

            if (userIdClaim != null)
            {
                var id = Guid.Parse(userIdClaim);
                var result = await _mediator.Send(new GetNewsFeedPostsRequest { Id = id, PageNumber = pageNumber, PageSize = pageSize });
                return Ok(result);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}