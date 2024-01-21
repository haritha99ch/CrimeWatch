using Application.Common.Validators;
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
        var result = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        return result.Handle(
            e =>
            {
                if (e.AccountId.Equals(id)) return true;
                validationError = UnauthorizedError.Create();
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            });
    }
}
