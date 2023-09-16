namespace CrimeWatch.Application.Contracts.Services;
internal interface IAuthenticationService
{
    (bool, string?) Authenticate();
}
