using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Galacticos.Application.DTOs.Comments.Validators
{
    public class CommentDtoValidator : AbstractValidator<CommentResponesDTO>
    {
        public CommentDtoValidator()
        {
            RuleFor(x => x.PostId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
            
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
        
    }
}