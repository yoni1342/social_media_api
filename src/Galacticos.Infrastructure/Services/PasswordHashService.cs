using Galacticos.Application.Services.Authentication;

namespace Galacticos.Infrastructure.Services;

public class PasswordHashService : IPasswordHashService
{
    public string HashPassword(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    public bool VerifyPassword(string candidatePassword, string hashedPassword)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(candidatePassword, hashedPassword);
        }
        catch (Exception)
        {
            return false;
        }
    }
}