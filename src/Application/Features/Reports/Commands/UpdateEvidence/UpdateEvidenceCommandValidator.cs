using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.UpdateEvidence;
internal sealed class UpdateEvidenceCommandValidator : ApplicationValidator<UpdateEvidenceCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public UpdateEvidenceCommandValidator(
            IAuthenticationService authenticationService,
            IRepository<Report, ReportId> reportRepository
        )
    {
        _authenticationService = authenticationService;
        _reportRepository = reportRepository;

        RuleFor(e => e)
            .MustAsync(IsAuthorized)
            .WithState(_ => validationError);
    }
    private async Task<bool> IsAuthorized(UpdateEvidenceCommand request, CancellationToken cancellationToken)
    {
        var authResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        AccountId currentUser = new(new());
        var isAuthenticated = authResult.Handle(
            e =>
            {
                currentUser = e.AccountId;
                return true;
            },
            e =>
            {
                validationError = e;
                return false;
            }
        );

        if (!isAuthenticated) return false;

        var evidence = await _reportRepository.GetOneAsync<EvidenceAuthorizationInfoById, EvidenceAuthorizationInfo>(
            new(request.ReportId, request.EvidenceId),
            cancellationToken);

        if (evidence is null)
        {
            validationError = EvidenceNotFoundError.Create(message: "Evidence is not found to edit.");
            return false;
        }

        if (evidence.AuthorId != null && evidence.AuthorId.Equals(currentUser)) return true;
        validationError = UnauthorizedError.Create(message: "You are not authorized to update this evidence.");
        return false;
    }
}
