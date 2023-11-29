using Application.Common.Validators;
using Application.Contracts.Services;
using Application.Features.Accounts.Queries.GetAccountById.Errors;
using FluentValidation;

namespace Application.Features.Accounts.Queries.GetAccountById;

public sealed class GetAccountByIdQueryValidator : ApplicationValidator<GetAccountByIdQuery>
{
    private readonly IAuthenticationService _authenticationService;

    public GetAccountByIdQueryValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        RuleFor(e => e.AccountId).MustAsync(IsAuthorizedAsync).WithState(e => validationError);
    }

    private async Task<bool> IsAuthorizedAsync(AccountId id, CancellationToken cancellationToken)
    {
        var result = await _authenticationService.GetAuthenticationResult(cancellationToken);
        return result.GetValue(
            e =>
            {
                if (e.AccountId.Equals(id))
                    return true;
                validationError = UnauthorizedError.Create();
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            }
        );
    }
}
