using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Likes
{
    public class LikeDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}