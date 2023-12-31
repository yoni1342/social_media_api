using MediatR;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Application.Features.Notifications.Commands
{
    public class CreateNotificationCommand : IRequest<CreateNotificationDTO>
    {
        public CreateNotificationDTO NotificationDTO { get; set; }
    }
}
