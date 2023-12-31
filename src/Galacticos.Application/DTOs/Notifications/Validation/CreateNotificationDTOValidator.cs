using FluentValidation;

namespace Galacticos.Application.DTOs.Notifications.Validation
{
    public class CreateNotificationDTOValidator : AbstractValidator<CreateNotificationDTO>
    {
        private int notificationsCount = 10;
        public CreateNotificationDTOValidator()
        {
            RuleFor(dto => dto.UserById)
                .NotEmpty()
                .WithMessage("Sender Id is required.");

            RuleFor(dto => dto.UserToId)
                .NotEmpty()
                .WithMessage("Reciever Id is required.");

            RuleFor(dto => dto.Content)
                .NotEmpty()
                .WithMessage("Content is required.")
                .WithMessage("Out of index");
        }
    }
}