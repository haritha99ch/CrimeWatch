namespace CrimeWatch.Application.Commands.ReportCommands.DeleteReport;
internal class DeleteReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    : IRequestHandler<DeleteReportCommand, bool>
{

    public async Task<bool> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        => await reportRepository.DeleteByIdAsync(request.Id);
}
