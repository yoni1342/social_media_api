using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.DTOs.Profile
{
    public class ProfileResponseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string Picture { get; set; }
        public string UserName { get; init; }
        public string Email { get; init; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Follow> Followers { get; set; }
        public ICollection<Follow> FollowedUsers { get; set; }

    }
}