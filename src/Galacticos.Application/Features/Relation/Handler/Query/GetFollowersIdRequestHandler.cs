using MediatR;
using Galacticos.Application.DTOs.Relations;
using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Features.Relation.Request.Query;

namespace Galacticos.Application.Features.Relation.Handler.Query
{
    public class GetFollowersIdRequestHandler : IRequestHandler<GetFollowersIdRequest, List<GetFollowersDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRelationRepository _relationRepository;

        public GetFollowersIdRequestHandler(IMapper mapper, IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _relationRepository = relationRepository;
        }

        public async Task<List<GetFollowersDTO>> Handle(GetFollowersIdRequest request, CancellationToken cancellationToken)
        {
            var relation = await _relationRepository.GetAllFollowersId(request.id);
            var relationDTO = _mapper.Map<List<GetFollowersDTO>>(relation);
            return relationDTO;
        }
    }
}