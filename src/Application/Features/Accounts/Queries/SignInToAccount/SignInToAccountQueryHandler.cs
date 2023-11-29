using Application.Contracts.Services;

namespace Application.Features.Accounts.Queries.SignInToAccount;

internal sealed class SignInToAccountQueryHandler : IQueryHandler<SignInToAccountQuery, string>
{
    private readonly IAuthenticationService _authenticationService;

    public SignInToAccountQueryHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public async Task<Result<string>> Handle(
        SignInToAccountQuery request,
        CancellationToken cancellationToken
    )
    {
        return await _authenticationService.AuthenticateAndGetTokenAsync(
            request.Email,
            request.Password,
            cancellationToken
        );
    }
}
