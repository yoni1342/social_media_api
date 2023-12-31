using Galacticos.Application.DTOs;
using MediatR;

namespace Galacticos.Application.Features.Tags.Request.Queries;
public class GetTagByIdQuery : IRequest<TagDto>
{
    public Guid TagId { get; set; }
}
