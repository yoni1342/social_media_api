using MediatR;
using Galacticos.Application.Features.Notifications.Commands;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Persistence.Contracts;
using AutoMapper;
using Galacticos.Domain.Entities;
using System.Numerics;

namespace Galacticos.Application.Features.Notifications.Handlers
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, CreateNotificationDTO>
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository _notificationRepository;

        public CreateNotificationCommandHandler(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<CreateNotificationDTO> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var createdNotification = await _notificationRepository.CreateNotification(request.NotificationDTO);
            return createdNotification;
        }

    }
}
