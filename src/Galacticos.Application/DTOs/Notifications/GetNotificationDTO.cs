using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Notifications
{
    public class GetNotificationDTO : BaseEntityDto
    {
        public Guid UserById { get; set; }
        public Guid UserToId { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
    }
}