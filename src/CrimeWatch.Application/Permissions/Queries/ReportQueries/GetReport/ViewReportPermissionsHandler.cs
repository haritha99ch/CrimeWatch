namespace CrimeWatch.Application.Permissions.Queries.ReportQueries.GetReport;
internal class ViewReportPermissionsHandler : IRequestHandler<GetReportPermissions, ReportPermissions>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpContext _httpContext => _httpContextAccessor.HttpContext!;

    public ViewReportPermissionsHandler(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<ReportPermissions> Handle(GetReportPermissions request, CancellationToken cancellationToken)
    {
        var userClaims = _httpContext.GetUserClaims();
        return userClaims.UserType switch
        {
            UserType.Guest => Task.FromResult(ReportPermissions.Moderated),
            UserType.Witness => Task.FromResult(ReportPermissions.Moderated),
            UserType.Moderator => Task.FromResult(ReportPermissions.FullAccess),
            _ => Task.FromResult(ReportPermissions.Moderated)
        };
    }
}
