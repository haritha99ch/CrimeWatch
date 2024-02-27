namespace Application.Features.Reports.Commands.AddEvidence;
public sealed record AddEvidenceCommand(
        ReportId ReportId,
        AccountId AuthorId,
        string Caption,
        string Description,
        string? No,
        string Street1,
        string? Street2,
        string City,
        string Province,
        IEnumerable<MediaUpload> MediaItems
    ) : ICommand<EvidenceDetails>;
