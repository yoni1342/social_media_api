using MediatR;
using Galacticos.Application.DTOs.Relations;
using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Features.Relation.Request.Query;

namespace Galacticos.Application.Features.Relation.Handler.Query
{
    public class GetFollowedIdsRequestHandler : IRequestHandler<GetFollowedIdsRequest, List<GetFollowersDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IRelationRepository _relationRepository;

        public GetFollowedIdsRequestHandler(IMapper mapper, IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _relationRepository = relationRepository;
        }

        public async Task<List<GetFollowersDTO>> Handle(GetFollowedIdsRequest request, CancellationToken cancellationToken)
        {
            var relation = await _relationRepository.GetAllFollowedIdsByUserId(request.id);
            
            var relationDTO = _mapper.Map<List<GetFollowersDTO>>(relation);

            return relationDTO;
        }
    }
}