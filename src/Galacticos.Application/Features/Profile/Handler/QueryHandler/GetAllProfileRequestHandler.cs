using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.Features.Profile.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using MediatR;

namespace Galacticos.Application.Features.Profile.Handler.QueryHandler
{
    public class GetAllProfileRequestHandler : IRequestHandler<GetAllProfileRequest, List<ProfileResponseDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllProfileRequestHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public Task<List<ProfileResponseDTO>> Handle(GetAllProfileRequest request, CancellationToken cancellationToken)
        {
            List<User> users = _userRepository.GetAllUsers();
            List<ProfileResponseDTO> profileResponseDTOs = _mapper.Map<List<ProfileResponseDTO>>(users);
            return Task.FromResult(profileResponseDTOs);
        }
    }
}