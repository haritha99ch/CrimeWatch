using CrimeWatch.Application.Shared;
using CrimeWatch.Domain.AggregateModels.AccountAggregate;

namespace CrimeWatch.Application.Contracts.Services;
public interface IAuthenticationService
{
    AuthenticationResult Authenticate();
    Task<string> AuthenticateAndGetToken(Account account, CancellationToken cancellationToken);
}
