using AutoMapper;
using Galacticos.Domain.Entities;
using Galacticos.Application.DTOs.Notifications;

namespace Galacticos.Application.Profiles
{
    public class NotificationMapping : Profile
    {
        public NotificationMapping()
        {
            CreateMap<Notification, GetNotificationDTO>().ReverseMap();
        }
    }
}