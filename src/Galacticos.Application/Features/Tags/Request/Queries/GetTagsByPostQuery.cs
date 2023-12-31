using Galacticos.Application.DTOs;
using MediatR;

namespace Galacticos.Application.Features.Tags.Request.Queries;
public class GetTagsByPostQuery : IRequest<List<TagDto>>
{
    public Guid PostId { get; set; }
}
