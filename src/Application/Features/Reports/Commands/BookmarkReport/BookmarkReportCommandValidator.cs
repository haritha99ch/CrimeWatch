using Application.Common.Validators;
using Application.Specifications.Reports;
using FluentValidation;

namespace Application.Features.Reports.Commands.BookmarkReport;
internal sealed class BookmarkReportCommandValidator : ApplicationValidator<BookmarkReportCommand>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IRepository<Report, ReportId> _reportRepository;

    public BookmarkReportCommandValidator(
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
    private async Task<bool> IsAuthorizedAsync(BookmarkReportCommand request, CancellationToken cancellationToken)
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
        if (!currentUser.Equals(request.AccountId))
        {
            validationError = UnauthorizedError.Create(message: "Mismatching current user and user in the request.");
            return false;
        }
        var report = await _reportRepository.GetOneAsync<ReportBookmarkInfoById, ReportBookmarkInfo>(
            new(request.ReportId, request.AccountId),
            cancellationToken);
        if (report is null)
        {
            validationError = ReportNotFoundError.Create(message: "Report not found to bookmark");
            return false;
        }
        if (!report.ReportStatus.Equals(Status.Approved) && !isModerator)
        {
            validationError = UnauthorizedError.Create(message: "Report is not approved.");
            return false;
        }
        if (!report.AlreadyBookmarked) return true;
        validationError = ReportAlreadyBookmarkError.Create(message: "Report is already bookmarked.");
        return false;
    }


}
