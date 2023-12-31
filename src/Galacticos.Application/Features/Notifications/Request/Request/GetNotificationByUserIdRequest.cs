using MediatR;
using System;
using System.Collections.Generic;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Application.Features.Notifications.Requests
{
    public class GetNotificationByUserIdRequest : IRequest<List<GetNotificationDTO>>
    {
        public Guid UserToId { get; set; }
    }
}
