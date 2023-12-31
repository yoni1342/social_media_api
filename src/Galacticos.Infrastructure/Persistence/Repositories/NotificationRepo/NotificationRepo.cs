using AutoMapper;
using Galacticos.Application.DTOs.Notifications;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Galacticos.Infrastructure.Persistence.Repositories.NotificationRepo
{
    public class NotificationRepo : INotificationRepository
    {
        private readonly ApiDbContext _context;
        private readonly IMapper _mapper;
        public NotificationRepo(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CreateNotificationDTO> CreateNotification(CreateNotificationDTO notificationDTO)
        {
            var notification = new Notification
            {
                UserToId = notificationDTO.UserToId,
                UserById = notificationDTO.UserById,
                Content = notificationDTO.Content
            };
            _context.notifications.Add(notification);
            await _context.SaveChangesAsync();
            return notificationDTO;
        }
        public async Task<List<GetNotificationDTO>> GetAllNotifications()
        {
            var notificationIds = await _context.notifications.ToListAsync();  // Get all notifications
            var notificationDTOs = _mapper.Map<List<GetNotificationDTO>>(notificationIds);
            return notificationDTOs;
        }
        public async Task<GetNotificationDTO> GetNotificationById(Guid notificationId)
        {
            var notification = await _context.notifications.FirstOrDefaultAsync(n => n.Id == notificationId);

            if (notification != null)
            {
                notification.IsRead = true;
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<GetNotificationDTO>(notification);
        }


        public async Task<List<GetNotificationDTO>> GetNotificationByUserId(Guid UserId)
        {
            var notifications = await _context.notifications.Where(n => n.UserToId == UserId && n.IsRead == false).ToListAsync();
            var notificationDTOs = _mapper.Map<List<GetNotificationDTO>>(notifications);
            // sort with date reverse
            notificationDTOs.Sort((x, y) => DateTime.Compare(y.CreatedAt, x.CreatedAt));
            return notificationDTOs;
        }
    }
}