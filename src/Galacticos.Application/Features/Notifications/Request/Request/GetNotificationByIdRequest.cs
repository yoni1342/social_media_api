using MediatR;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Application.Features.Notifications.Requests
{
    public class GetNotificationByIdRequest : IRequest<GetNotificationDTO>
    {
        public Guid NotificationId { get; set; }
    }
}
