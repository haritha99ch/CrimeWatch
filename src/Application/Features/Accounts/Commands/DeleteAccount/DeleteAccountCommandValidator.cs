using Application.Common.Validators;
using Application.Contracts.Services;
using FluentValidation;

namespace Application.Features.Accounts.Commands.DeleteAccount;
public sealed class DeleteAccountCommandValidator : ApplicationValidator<DeleteAccountCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public DeleteAccountCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        RuleFor(e => e.AccountId).MustAsync(IsAuthorizedAsync).WithState(_ => validationError);
    }

    private async Task<bool> IsAuthorizedAsync(
            AccountId accountId,
            CancellationToken cancellationToken
        )
    {
        var result = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        return result.Handle(
            e =>
            {
                if (accountId.Equals(e.AccountId)) return true;
                validationError = UnauthorizedError.Create(message: "You are not authorized to delete this account");
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            });
    }
}
