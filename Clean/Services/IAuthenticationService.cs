using System.Security.Claims;

namespace Clean.Services
{
    public interface IAuthenticationService
    {
        bool ValidateCredentials(string username, string password);

        ClaimsIdentity CreateIdentity(string username);
    }
}
