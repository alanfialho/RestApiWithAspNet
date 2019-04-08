using DatabaseIntegration.Model;

namespace DatabaseIntegration.Services
{
    public interface IAuthenticationService
    {
        Authentication Authenticate(User user);
    }
}