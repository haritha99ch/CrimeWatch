using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.ApproveEvidence;
internal sealed class ApproveEvidenceCommandValidator : ApplicationValidator<ApproveEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ApproveEvidenceCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(ApproveEvidenceCommand request, CancellationToken cancellationToken)
    {
        var authResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var currentUser = new AccountId(new());
        var isModerator = false;
        var isAuthenticated = authResult.Handle(
            e =>
            {
                currentUser = e.AccountId;
                isModerator = e.IsModerator;
                return true;
            },
            e =>
            {
                validationError = e;
                return false;
            });
        if (!isAuthenticated) return false;
        if (!isModerator)
        {
            validationError = UnauthorizedError.Create(message: "Only moderators can approve Evidence.");
            return false;
        }
        var report = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);
        if (report is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "Evidence is not found to approve.");
            return false;
        }
        if (!report.Status.Equals(Status.UnderReview))
        {
            validationError =
                EvidenceIsNoUnderReviewError.Create(message: "Evidence must be under reviewed to approve.");
            return false;
        }
        if (report.ModeratorId == null || report.ModeratorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError.Create(message: "Only moderator reviewing the Evidence can approve.");
        return false;
    }

}
