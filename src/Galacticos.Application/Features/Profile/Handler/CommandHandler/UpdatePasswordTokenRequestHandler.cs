using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ErrorOr;
using Galacticos.Application.Common.Interface.Authentication;
using Galacticos.Application.Features.Profile.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Application.Services.Authentication;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Profile.Handler.CommandHandler
{
    public class UpdatePasswordTokenRequestHandler : IRequestHandler<UpdatePasswordTokenRequest, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IJwtTokenValidation _jwtTokenValidation;
        private readonly IMapper _mapper;

        public UpdatePasswordTokenRequestHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService, IJwtTokenValidation jwtTokenValidation)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
            _jwtTokenValidation = jwtTokenValidation;
        }

        public Task<ErrorOr<string>> Handle(UpdatePasswordTokenRequest request, CancellationToken cancellationToken)
        {
            var email = _jwtTokenValidation.ExtractEmailFromToken(request.Token);

            if (email == null)
            {
                return Task.FromResult<ErrorOr<string>>(Errors.User.InvalidUser);
            }
            
            var user = _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return Task.FromResult<ErrorOr<string>>(Errors.User.UserNotFound);
            }

            if (request.updatePasswordTokenRequestDTO.NewPassword != request.updatePasswordTokenRequestDTO.ConfirmNewPassword)
            {
                return Task.FromResult<ErrorOr<string>>(Errors.User.PasswordNotMatch);
            }

            var userToEdit = _mapper.Map(request, user);
            userToEdit.Password = _passwordHashService.HashPassword(request.updatePasswordTokenRequestDTO.NewPassword);
            _userRepository.EditUser(userToEdit);
            return Task.FromResult<ErrorOr<string>>("Password updated successfully");
        }
    }
}