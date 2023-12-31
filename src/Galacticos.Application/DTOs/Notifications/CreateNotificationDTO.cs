namespace Galacticos.Application.DTOs.Notifications
{
    public class CreateNotificationDTO
    {
        public Guid UserToId { get; set; }
        public Guid UserById { get; set; }
        public string Content { get; set; }
    }
}