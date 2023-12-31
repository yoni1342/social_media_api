using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class PostTag : BaseEntity
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; } = null!;
        public Guid TagId { get; set; }
        public Tag Tag { get; set; } = null!;       
    }
}