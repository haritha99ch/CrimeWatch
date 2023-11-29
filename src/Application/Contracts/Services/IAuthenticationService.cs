using Application.Selectors;

namespace Application.Contracts.Services;

public interface IAuthenticationService
{
    Task<Result<string>> AuthenticateAndGetTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken
    );
    Task<string> RefreshToken(string token, string refreshToken);

    Task<Result<AccountAuthenticationInfo>> GetAuthenticationResult(
        CancellationToken cancellationToken
    );
}
