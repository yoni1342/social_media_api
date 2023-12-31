namespace Galacticos.Application.Services.Authentication;

public interface IAuthenticationService{
    AuthenticationResult Login(string username, string password);
    AuthenticationResult Register(string FirstName, string LastName, string username, string password, string email);
}