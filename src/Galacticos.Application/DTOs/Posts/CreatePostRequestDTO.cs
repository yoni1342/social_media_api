using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Galacticos.Application.DTOs.Posts
{
    public class CreatePostRequestDTO
    {
        public string Caption { get; set; } = null!;
        public IFormFile? Image { get; set; }
    }
}