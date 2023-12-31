using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace Galacticos.Application.DTOs.Comments.Validators
{
    public class CommentCommandValidator : AbstractValidator<CreateCommentRequestDTO>
    {
        public CommentCommandValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }  
    }
}