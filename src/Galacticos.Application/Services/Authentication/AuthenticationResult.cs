using Galacticos.Application.DTOs.Users;

namespace Galacticos.Application.Services.Authentication;

public record AuthenticationResult(
    UserDto UserDto,
    string Token
);