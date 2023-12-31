using ErrorOr;
using MediatR;

namespace Galacticos.Application.Features.Auth.Handlers.CommandHandlers
{
    public class ForgotPasswordRequest : IRequest<ErrorOr<string>>
    {
        public string Email { get; set; } = null!;
    }
}