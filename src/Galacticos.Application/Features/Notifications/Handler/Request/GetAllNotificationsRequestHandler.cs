using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Features.Notifications.Requests;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Features.Notifications.Handlers
{
    public class GetAllNotificationsRequestHandler : IRequestHandler<GetAllNotificationsRequest, List<GetNotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetAllNotificationsRequestHandler(INotificationRepository notificationRepository, IMapper mapper)
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
        }

        public async Task<List<GetNotificationDTO>> Handle(GetAllNotificationsRequest request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetAllNotifications();
            var notificationDTOs = _mapper.Map<List<GetNotificationDTO>>(notifications);
            return notificationDTOs;
        }
    }
}
