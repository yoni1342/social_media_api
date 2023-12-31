using MediatR;
using Galacticos.Application.DTOs.Relations;
using AutoMapper;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Features.Relation.Request.Command;
using Galacticos.Application.DTOs.Relations.Validators;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Features.Relation.Handler.Command
{
    public class FollowCommandHandler : IRequestHandler<FollowCommand, GetFollowersDTO>
    {
        private readonly IMapper _mapper;
        private readonly IRelationRepository _relationRepository;

        public FollowCommandHandler(IMapper mapper, IRelationRepository relationRepository)
        {
            _mapper = mapper;
            _relationRepository = relationRepository;
        }

        public async Task<GetFollowersDTO> Handle(FollowCommand request, CancellationToken cancellationToken)
        {
            var validator = new RelationDTOValidator();
            var validation = await validator.ValidateAsync(request.RelationDTO);
            if (!validation.IsValid)
            {
                throw new Exception();
            }

            var relation = _mapper.Map<Follow>(request.RelationDTO);
            var user = await _relationRepository.Follow(relation.FollowerId, relation.FollowedUserId);

            
            
            if (user == null)
            {
                return null;
            }

            var relationDTO = _mapper.Map<GetFollowersDTO>(user);

            return relationDTO;
        }
    }
}