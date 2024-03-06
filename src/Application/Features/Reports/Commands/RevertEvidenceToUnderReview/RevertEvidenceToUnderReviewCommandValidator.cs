using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.RevertEvidenceToUnderReview;
internal sealed class RevertEvidenceToUnderReviewCommandValidator
    : ApplicationValidator<RevertEvidenceToUnderReviewCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RevertEvidenceToUnderReviewCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(
            RevertEvidenceToUnderReviewCommand request,
            CancellationToken cancellationToken
        )
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
            validationError = UnauthorizedError.Create(message: "Only moderators can review evidence.");
            return false;
        }
        var evidence = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);
        if (evidence is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "Evidence is not found to review.");
            return false;
        }
        if (evidence.Status.Equals(Status.UnderReview))
        {
            validationError = EvidenceIsUnderReviewError.Create();
            return false;
        }
        if (evidence.ModeratorId == null || evidence.ModeratorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError
            .Create(message: "Only moderator reviewing the evidence can revert to review.");
        return false;
    }

}
