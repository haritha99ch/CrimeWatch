using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.ReportCommands.DeleteReport;
internal class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, bool>
{
    private readonly IRepository<Report, ReportId> _reportRepository;

    public DeleteReportCommandHandler(IRepository<Report, ReportId> reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<bool> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        => await _reportRepository.DeleteByIdAsync(request.Id);
}
