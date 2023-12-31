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
using Galacticos.Domain.Entities;
using Galacticos.Domain.Errors;
using MediatR;

namespace Galacticos.Application.Features.Profile.Handler.CommandHandler
{
    public class UpdatePasswordRequestHandler : IRequestHandler<UpdatePasswordRequest, ErrorOr<string>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IMapper _mapper;
        public UpdatePasswordRequestHandler(IUserRepository userRepository, IMapper mapper, IPasswordHashService passwordHashService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHashService = passwordHashService;
        }

        public Task<ErrorOr<string>> Handle(UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetUserById(request.UserId);

            if (user == null)
            {
                return Task.FromResult<ErrorOr<string>>(Errors.User.UserNotFound);
            }

            if (request.UpdatePasswordRequestDTO.NewPassword != request.UpdatePasswordRequestDTO.ConfirmNewPassword)
            {
                return Task.FromResult<ErrorOr<string>>(Errors.User.PasswordNotMatch);
            }
            
            var userToEdit = _mapper.Map(request, user);
            userToEdit.Password = _passwordHashService.HashPassword(request.UpdatePasswordRequestDTO.NewPassword);
            _userRepository.EditUser(userToEdit);
            return Task.FromResult<ErrorOr<string>>("Password updated successfully");
            }
        }
    }
