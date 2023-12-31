using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class Comment : BaseEntity
    {   
        public required Guid UserId { get; set; }
        public required Guid PostId { get; set; }
        public string Content { get; set; } = null!;
        public virtual Post Post { get; set; }
    }
}