using MediatR;
using Galacticos.Application.Features.Notifications.Requests;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Persistence.Contracts;
using AutoMapper;

namespace Galacticos.Application.Features.Notifications.Handler.Request
{
    public class GetNotificationByIdRequestHandler : IRequestHandler<GetNotificationByIdRequest, GetNotificationDTO>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;

        public GetNotificationByIdRequestHandler(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            _notificationRepository = notificationRepository;
        }

        public async Task<GetNotificationDTO> Handle(GetNotificationByIdRequest request, CancellationToken cancellationToken)
        {
            var notification = await _notificationRepository.GetNotificationById(request.NotificationId);
            return _mapper.Map<GetNotificationDTO>(notification);
        }
    }
}