using FluentValidation;

namespace Galacticos.Application.DTOs.Relations.Validators
{
    public class RelationDTOValidator : AbstractValidator<RelationDTO>
    {
        public RelationDTOValidator()
        {

            RuleFor(dto => dto.FollowerId)
            .NotEmpty()
            .WithMessage("FollowerId is required.");


            RuleFor(dto => dto.FollowedUserId)
                .NotEmpty()
                .WithMessage("FollowedUserId is required.");

            RuleFor(dto => dto.FollowedUserId)
                .NotEqual(dto => dto.FollowerId)
                .WithMessage("FollowerId and FollowedUSerId cannot be the same.");

        }
    }
}