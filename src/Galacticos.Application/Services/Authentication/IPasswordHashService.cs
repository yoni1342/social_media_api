namespace Galacticos.Application.Services.Authentication
{
    public interface IPasswordHashService{
        string HashPassword(string plainPassword);
        bool VerifyPassword(string candidatePassword, string hashedPassword);
    }
}