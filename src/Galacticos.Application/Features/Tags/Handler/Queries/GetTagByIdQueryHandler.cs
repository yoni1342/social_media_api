using AutoMapper;
using Galacticos.Application.DTOs;
using Galacticos.Application.Features.Tags.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Galacticos.Application.Tags.Queries
{
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<TagDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetById(request.TagId);
            var tagDto = _mapper.Map<TagDto>(tag); // Use AutoMapper to map entity to DTO
            return tagDto;
        }
    }
}
