using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs.Tags;
using Galacticos.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Galacticos.Application.DTOs.Posts
{
    public class UpdatePostRequestDTO
    {

        public string? Caption {get; set;} = "";
        public IFormFile? Image {get; set;}
    }
}