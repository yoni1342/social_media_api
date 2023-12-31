using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class User : BaseEntity

    {
        public User()
        {
            Posts = new HashSet<Post>();
            Notifications = new HashSet<Notification>();
            Followers = new HashSet<Follow>();
            FollowedUsers = new HashSet<Follow>();
        }
        public string FirstName { set; get; } = null!;
        public string LastName { set; get; } = null!;
        public string UserName { set; get; } = null!;
        public string Email { set; get; } = null!;
        public string Password { set; get; }
        public string? Bio { set; get; } = "";
        public string? Picture  { set; get; } = "";
        public bool Verified { set; get; } = false;
 
        public ICollection<Post> Posts { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> FollowedUsers { get; set; }

    }
}