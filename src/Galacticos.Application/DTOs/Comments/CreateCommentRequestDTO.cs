using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Galacticos.Application.DTOs.Comments
{
    public class CreateCommentRequestDTO
    {
        public string Content { get; set; } = null!;
    }
}