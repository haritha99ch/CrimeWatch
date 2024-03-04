using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.ModerateEvidence;
internal sealed class ModerateEvidenceCommandValidator : ApplicationValidator<ModerateEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public ModerateEvidenceCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(ModerateEvidenceCommand request, CancellationToken cancellationToken)
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
            }
        );
        if (!isAuthenticated) return false;
        if (!isModerator)
        {
            validationError = UnauthorizedError.Create(message: "Only moderators can moderate evidence.");
            return false;
        }
        if (!currentUser.Equals(request.AccountId))
        {
            validationError = UnauthorizedError.Create(message: "Mismatching current user and user in the request.");
            return false;
        }
        var evidence = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);
        if (evidence is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "Evidence not found to moderate.");
            return false;
        }
        if (evidence.Status.Equals(Status.Pending)) return true;
        validationError = EvidenceAlreadyModeratedError.Create();
        return false;
    }
}
