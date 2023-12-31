using MediatR;
using Galacticos.Application.Features.Notifications.Requests;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using AutoMapper;

namespace Galacticos.Application.Features.Notifications.Handler.Request
{
    public class GetNotificationByUserIdRequestHandler : IRequestHandler<GetNotificationByUserIdRequest, List<GetNotificationDTO>>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationByUserIdRequestHandler(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<List<GetNotificationDTO>> Handle(GetNotificationByUserIdRequest request, CancellationToken cancellationToken)
        {
            var notifications = await _notificationRepository.GetNotificationByUserId(request.UserToId);
            var notificationDTOs = _mapper.Map<List<GetNotificationDTO>>(notifications);

            return notificationDTOs;
        }

    }
}