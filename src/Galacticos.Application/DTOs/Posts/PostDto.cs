using Galacticos.Application.DTOs;
using Galacticos.Application.DTOs.Common;

namespace Galacticos.Application.DTOs.Posts;
public class PostDto : BaseEntityDto
{
    public string? Caption { get; set; }
    public string? Image { get; set; }
}