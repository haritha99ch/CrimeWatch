using Application.Common.Validators;
using Application.Contracts.Services;
using FluentValidation;

namespace Application.Features.Reports.Commands.CreateReport;
internal sealed class CreateReportCommandValidator : ApplicationValidator<CreateReportCommand>
{
    private readonly IAuthenticationService _authenticationService;

    public CreateReportCommandValidator(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        RuleFor(e => e.AuthorId).MustAsync(IsAuthorizedAsync).WithState(_ => validationError);
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
                if (e.AccountId.Equals(accountId)) return true;
                validationError = UnauthorizedError.Create(
                    message: "You are not authorized to create a report for another user.");
                return false;
            },
            e =>
            {
                validationError = e;
                return false;
            });
    }
}
