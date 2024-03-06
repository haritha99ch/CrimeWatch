using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.DeclineEvidence;
internal class DeclineEvidenceCommandValidator : ApplicationValidator<DeclineEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeclineEvidenceCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(DeclineEvidenceCommand request, CancellationToken cancellationToken)
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
            validationError = UnauthorizedError.Create(message: "Only moderators can decline evidence.");
            return false;
        }
        var evidence = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);
        if (evidence is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "Evidence is not found to decline.");
            return false;
        }

        if (!evidence.Status.Equals(Status.Approved) && !evidence.Status.Equals(Status.UnderReview))
        {
            validationError = EvidenceIsNoUnderReviewError
                .Create(message: "Evidence must be under reviewed to declined.");
            return false;
        }

        if (evidence.ModeratorId == null || evidence.ModeratorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError.Create(message: "Only moderator reviewing the evidence can decline.");
        return false;
    }
}
