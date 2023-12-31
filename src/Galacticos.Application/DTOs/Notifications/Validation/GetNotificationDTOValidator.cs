using FluentValidation;

namespace Galacticos.Application.DTOs.Notifications.Validation
{
    public class GetNotificationDTOValidator : AbstractValidator<GetNotificationDTO>
    {
        private int notificationsCount = 10;
        public GetNotificationDTOValidator()
        {
            RuleFor(dto => dto.UserById)
                .NotEmpty()
                .WithMessage("Sender is required.");

            RuleFor(dto => dto.Content)
                .NotEmpty()
                .WithMessage("Content is required.")
                .WithMessage($"Content cannot exceed {notificationsCount} characters.");

            RuleFor(dto => dto.CreatedAt)
                .NotEmpty()
                .WithMessage("DateTime is required.");
        }
    }
}