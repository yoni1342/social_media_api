using AutoMapper;
using Galacticos.Application.DTOs;
using Galacticos.Application.Features.Tags.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Galacticos.Application.Tags.Queries
{
    public class GetTagsByPostQueryHandler : IRequestHandler<GetTagsByPostQuery, List<TagDto>>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetTagsByPostQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<List<TagDto>> Handle(GetTagsByPostQuery request, CancellationToken cancellationToken)
        {
            var tags = await _tagRepository.GetTagsByPost(request.PostId);
            var tagDtos = _mapper.Map<List<TagDto>>(tags); // Use AutoMapper to map entities to DTOs
            return tagDtos;
        }
    }
}
