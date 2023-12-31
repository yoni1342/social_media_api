using ErrorOr;
using Galacticos.Application.Services.Authentication;
using MediatR;

namespace Galacticos.Application.Features.Auth.Requests.Commands;

public class RegisterCommand : IRequest<ErrorOr<string>>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? Bio { get; set; } = "";
    public string? Picture { get; set; } = "";
    public string Password { get; set; } = null!;
    public string? ConfirmPassword { get; set; } = null!;
}
