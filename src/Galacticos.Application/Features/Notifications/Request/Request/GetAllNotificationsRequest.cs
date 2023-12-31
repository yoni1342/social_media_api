using Galacticos.Application.DTOs.Notifications;
using MediatR;
using System.Collections.Generic;

namespace Galacticos.Application.Features.Notifications.Requests
{
    public class GetAllNotificationsRequest : IRequest<List<GetNotificationDTO>>  // Change the return type here
    {
    }
}
