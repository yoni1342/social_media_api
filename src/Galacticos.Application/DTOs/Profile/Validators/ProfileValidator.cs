using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Galacticos.Application.Features.Profile.Request.Commands;

namespace Galacticos.Application.DTOs.Profile.Validators
{
    public class ProfileValidator : AbstractValidator<EditProfileRequest>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.EditProfileRequestDTO.FirstName)
                .MaximumLength(50)
                .WithMessage("First name cannot be longer than 50 characters");

            RuleFor(x => x.EditProfileRequestDTO.LastName)
                .MaximumLength(50)
                .WithMessage("Last name cannot be longer than 50 characters");

            RuleFor(x => x.EditProfileRequestDTO.Bio)
                .MaximumLength(500)
                .WithMessage("Bio cannot be longer than 500 characters");

        }
        
    }
}