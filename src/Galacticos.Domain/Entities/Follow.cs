using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class Follow : BaseEntity
    {
        public Guid FollowerId { get; set; }
        public User Follower { get; set; } // Navigation property
        
        public Guid FollowedUserId { get; set; }
        public User FollowedUser { get; set; } // Navigation property
    }
}