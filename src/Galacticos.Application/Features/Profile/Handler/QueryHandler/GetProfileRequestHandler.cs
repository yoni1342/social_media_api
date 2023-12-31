using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.Features.Profile.Request.Queries;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using MediatR;
using Galacticos.Domain.Errors;
namespace Galacticos.Application.Features.Profile.Handler.QueryHandler
{
    public class GetProfileRequestHandler : IRequestHandler<GetProfileRequest, ErrorOr<ProfileResponseDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        
        public GetProfileRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            Console.WriteLine("GetProfileRequestHandler");
        }
        public Task<ErrorOr<ProfileResponseDTO>> Handle(GetProfileRequest request, CancellationToken cancellationToken)
        {
            User user = _userRepository.GetUserById(request.UserId)!;
            if (user == null)
            {
                return Task.FromResult<ErrorOr<ProfileResponseDTO>>(Errors.User.UserNotFound);
            }
            ProfileResponseDTO profileResponseDTO = _mapper.Map<ProfileResponseDTO>(user);
            return Task.FromResult<ErrorOr<ProfileResponseDTO>>(profileResponseDTO);
        }
    }
}