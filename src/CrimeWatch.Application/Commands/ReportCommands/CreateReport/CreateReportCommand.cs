namespace CrimeWatch.Application.Commands.ReportCommands.CreateReport;
public sealed record CreateReportCommand(
        WitnessId WitnessId,
        string Title,
        string Description,
        Location Location,
        IFormFile MediaItem
    ) : IRequest<Report>;
