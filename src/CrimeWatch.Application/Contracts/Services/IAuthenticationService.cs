using CrimeWatch.Application.Shared;

namespace CrimeWatch.Application.Contracts.Services;
public interface IAuthenticationService
{
    AuthenticationResult Authenticate();
    Task<string> AuthenticateAndGetToken(Account account, CancellationToken cancellationToken);
}
