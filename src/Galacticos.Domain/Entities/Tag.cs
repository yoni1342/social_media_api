using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities;

public class Tag : BaseEntity{
    public string Name { get; set; }
    public ICollection<PostTag> PostTags { get; set; }
}