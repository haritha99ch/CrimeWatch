using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.RemoveEvidence;
internal sealed class RemoveEvidenceCommandValidator : ApplicationValidator<RemoveEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RemoveEvidenceCommandValidator(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;

        RuleFor(e => e)
            .MustAsync(IsAuthorizedAsync)
            .WithState(_ => validationError);
    }
    private async Task<bool> IsAuthorizedAsync(RemoveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var authenticationResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        AccountId currentUser = new(new());
        var isCurrentUserModerator = false;
        var isAuthenticated = authenticationResult.Handle(
            e =>
            {
                currentUser = e.AccountId;
                isCurrentUserModerator = e.IsModerator;
                return true;
            },
            e =>
            {
                validationError = e;
                return false;
            });
        if (!isAuthenticated) return false;

        var evidence = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);
        if (evidence is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "No evidence is found to be deleted");
            return false;
        }

        if (isCurrentUserModerator) return true;
        if (evidence.AuthorId != null && evidence.AuthorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError.Create("You are not authorized to delete this evidence.");
        return false;
    }


}
