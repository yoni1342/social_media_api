using Galacticos.Application.DTOs;
using MediatR;

namespace Galacticos.Application.Features.Tags.Request.Queries;
public class SearchTagsQuery : IRequest<List<TagDto>>
{
    public string SearchTerm { get; set; }
}
