using Application.Common.Validators;
using Application.Contracts.Services;
using FluentValidation;

namespace Application.Features.Accounts.Commands.UpdateWitnessAccount;
public sealed class UpdateWitnessAccountCommandValidator
    : ApplicationValidator<UpdateWitnessAccountCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public UpdateWitnessAccountCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        RuleFor(e => e.AccountId).MustAsync(IsAuthorizedAsync).WithState(e => validationError);
    }

    private async Task<bool> IsAuthorizedAsync(AccountId accountId, CancellationToken token)
    {
        var result = await _authenticationService.GetAuthenticationResultAsync(token);
        return result.Handle(
            e =>
            {
                if (e.AccountId.Equals(accountId)) return true;
                validationError = UnauthorizedError.Create(message: "You are not authorized to update this account.");
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            });
    }
}
