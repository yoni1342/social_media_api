using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Galacticos.Application.DTOs.Profile;
using Galacticos.Application.Features.Profile.Request.Commands;
using Galacticos.Application.Persistence.Contracts;
using MediatR;
using Galacticos.Domain.Errors;
using AutoMapper;
using Galacticos.Domain.Entities;
using Galacticos.Application.DTOs.Profile.Validators;
using Galacticos.Application.Services.ImageUpload;

namespace Galacticos.Application.Features.Profile.Handler.CommandHandler
{
    public class EditProfileRequestHandler : IRequestHandler<EditProfileRequest, ErrorOr<ProfileResponseDTO>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        public EditProfileRequestHandler(IUserRepository userRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }
        public Task<ErrorOr<ProfileResponseDTO>> Handle(EditProfileRequest request, CancellationToken cancellationToken)
        {
            var user =  _userRepository.GetUserById(request.UserId);
            
            if (user == null)
            {
                Console.WriteLine("User not found");
                return Task.FromResult<ErrorOr<ProfileResponseDTO>>(Errors.User.UserNotFound);
            }

            var validator = new ProfileValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return Task.FromResult<ErrorOr<ProfileResponseDTO>>(Errors.User.InvalidUser);
            }
            
            var picture = _cloudinaryService.UploadImageAsync(request.EditProfileRequestDTO.Picture!).Result;
            
            var userToEdit = _mapper.Map(request.EditProfileRequestDTO, user);
            userToEdit.Picture = picture;

            var editedUser = _userRepository.EditUser(userToEdit);
            var profileResponseDTO = _mapper.Map<ProfileResponseDTO>(editedUser);
            return Task.FromResult<ErrorOr<ProfileResponseDTO>>(profileResponseDTO);
        }
    }
}