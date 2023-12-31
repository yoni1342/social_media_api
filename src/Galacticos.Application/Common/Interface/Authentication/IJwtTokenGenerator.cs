using Galacticos.Domain.Entities;

namespace Galacticos.Application.Common.Interface.Authentication;

public interface IJwtTokenGenerator{
    string GenerateToken(User user);
    string GenerateRecoveryToken(string email);
}