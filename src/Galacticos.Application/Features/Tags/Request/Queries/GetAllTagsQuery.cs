using Galacticos.Application.DTOs;
using MediatR;

namespace Galacticos.Application.Features.Tags.Request.Queries;
public class GetAllTagsQuery : IRequest<List<TagDto>>
{
}
