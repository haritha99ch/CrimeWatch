using Microsoft.AspNetCore.Http;

namespace Application.Features.Reports.Commands.CreateReport;
public sealed record CreateReportCommand(
        AccountId AuthorId,
        string Caption,
        string Description,
        string No,
        string Street1,
        string Street2,
        string City,
        string Province,
        List<ViolationType> ViolationTypes,
        IFormFile MediaItem
    ) : ICommand<Report>;
