using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Galacticos.Application.DTOs.Posts.Validator
{
    public class UpdatePostDtoValidator : AbstractValidator<PostDto>
    {
        public UpdatePostDtoValidator()
        {
            RuleFor(p => p.Caption)
                .NotEmpty()
                .When(p => string.IsNullOrEmpty(p.Image));

            RuleFor(p => p.Image)
                .NotEmpty()
                .When(p => string.IsNullOrEmpty(p.Caption));
        }
    }
}