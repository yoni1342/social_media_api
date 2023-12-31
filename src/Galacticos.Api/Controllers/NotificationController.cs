using System.Security.Claims;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.Features.Notifications.Requests;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Galacticos.Api.Controllers
{
    [Route("api/[controller]")]
    public class NotificationController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public NotificationController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{notificationId}")]
        public async Task<ActionResult<GetNotificationDTO>> GetNotificationById(Guid notificationId)
        {
            var notification = await _mediator.Send(new GetNotificationByIdRequest { NotificationId = notificationId });

            if (notification == null)
            {
                return NotFound();
            }

            return Ok(notification);
        }


        [HttpGet("user")]
        public async Task<IActionResult> GetNotificationsByUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext!.User.FindFirstValue("uid");
            
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim);
            var request = new GetNotificationByUserIdRequest { UserToId = userId };
            var notifications = await _mediator.Send(request);

            return Ok(notifications);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllNotifications()
        {
            var request = new GetAllNotificationsRequest();
            var notifications = await _mediator.Send(request);

            return Ok(notifications);
        }
    }
}