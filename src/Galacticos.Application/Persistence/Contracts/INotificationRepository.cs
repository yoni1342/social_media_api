using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Application.Persistence.Contracts
{
    public interface INotificationRepository
    {
        Task<CreateNotificationDTO> CreateNotification(CreateNotificationDTO notification);
        Task<GetNotificationDTO> GetNotificationById(Guid NotificationId);
        Task<List<GetNotificationDTO>> GetNotificationByUserId(Guid UserId);
        Task<List<GetNotificationDTO>> GetAllNotifications(); // Change the return type here
    }
}
