﻿namespace Application.Features.Reports.Commands.DeleteReport;
public sealed class DeleteReportCommandHandler : ICommandHandler<DeleteReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeleteReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<Result<bool>> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var deleted = await _reportRepository.DeleteByIdAsync(request.ReportId, cancellationToken);
        if (!deleted) return ReportCouldNotBeDeletedError.Create();
        return deleted;
    }
}
