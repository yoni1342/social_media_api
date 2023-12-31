using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Domain.Entities.Common;

namespace Galacticos.Domain.Entities
{
    public class Post : BaseEntity
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }
        public Guid UserId { get; set; }
        public string Caption { get; set; } = null!;
        public string Image { get; set; } = "";
        public virtual User user { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<PostTag> PostTags { get; set; }

        public bool CanBeEditedBy(User currentUser)
        {
            // Only the post's author can edit it
            return UserId == currentUser.Id;
        }

        public bool CanBeDeletedBy(User currentUser)
        {
            // Only the post's author can delete it
            return UserId == currentUser.Id;
        }
    }
}