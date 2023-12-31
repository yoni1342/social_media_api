using FluentValidation;
using Galacticos.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galacticos.Application.DTOs.Posts.Validator
{
    public class CreatePostDtoValidator : AbstractValidator<PostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(p => p.Caption)
                .NotEmpty()
                .When(p => p.Image == null);

            RuleFor(p => p.Image)
                .NotEmpty()
                .When(p => string.IsNullOrEmpty(p.Caption));
        }
    }
}
