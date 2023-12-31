using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Likes
{
    public class LikeResponseDto : BaseEntityDto
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
    }
}