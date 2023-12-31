using MediatR;
using Galacticos.Application.DTOs.Relations;
using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Features.Relation.Request.Command;
using Galacticos.Application.DTOs.Relations.Validators;

namespace Galacticos.Application.Features.Relation.Handler.Command
{
    public class UnFollowCommandHandler : IRequestHandler<UnFollowCommand, GetFollowersDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRelationRepository _relationRepository;

        public UnFollowCommandHandler(IMapper mapper, IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _relationRepository = relationRepository;
        }

        public async Task<GetFollowersDTO> Handle(UnFollowCommand request, CancellationToken cancellationToken)
        {
            var validator = new RelationDTOValidator();
            var validation = await validator.ValidateAsync(request.RelationDTO);
            if (!validation.IsValid)
            {
                throw new Exception();
            }

            var relation = await _relationRepository.Get(request.RelationDTO.FollowerId, request.RelationDTO.FollowedUserId);
            var result = await _relationRepository.UnFollow(relation.FollowerId, relation.FollowedUserId);

            if (result == null)
            {
                return null;
            }

            var relationDTO = _mapper.Map<GetFollowersDTO>(result);

            return relationDTO;
        }
    }
}