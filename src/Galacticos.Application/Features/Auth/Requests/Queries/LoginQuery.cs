using ErrorOr;
using Galacticos.Application.Services.Authentication;
using MediatR;

namespace Galacticos.Application.Features.Auth.Requests.Queries;

public record LoginQuery
(
    string? UserName = "",
    string? Email = "",
    string Password = ""
) : IRequest<ErrorOr<AuthenticationResult>>;