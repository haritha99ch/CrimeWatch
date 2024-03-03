using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.RemoveBookmark;
internal sealed class RemoveBookmarkCommandValidator : ApplicationValidator<RemoveBookmarkCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public RemoveBookmarkCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(RemoveBookmarkCommand request, CancellationToken cancellationToken)
    {
        var authResult = await _authenticationService.GetAuthenticationResultAsync(cancellationToken);
        var currentUser = new AccountId(new());
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
        if (!currentUser.Equals(request.AccountId))
        {
            validationError = UnauthorizedError.Create(message: "Mismatching current user and user in the request.");
            return false;
        }
        var report = await _reportRepository.GetOneAsync<ReportBookmarkInfoById, ReportBookmarkInfo>(
            new(request.ReportId, currentUser),
            cancellationToken);
        if (report is null)
        {
            validationError = ReportNotFoundError.Create(message: "Report not found to remove bookmark");
            return false;
        }
        if (report.AlreadyBookmarked) return true;
        validationError = ReportNotBookmarkedError.Create();
        return false;
    }
}
