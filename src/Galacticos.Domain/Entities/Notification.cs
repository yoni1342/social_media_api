using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public Guid UserById { get; set; }
        public User UserBy { get; set; } = null!;
        public Guid UserToId { get; set; }
        public User UserTo { get; set; } = null!;
        public string Content { get; set; } = "";
        public bool IsRead { get; set; } = false;
    }
}