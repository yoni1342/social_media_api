using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Comments
{
    public class CommentResponesDTO : BaseEntityDto
    {
        public string Content {get; set;} = null!;
        public Guid PostId {get; set;}
        public Guid UserId {get; set;}
        
    }
}