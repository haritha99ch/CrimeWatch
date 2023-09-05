using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddEvidenceToReport;
internal class CreateEvidenceCommandHandler : IRequestHandler<CreateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;

    public CreateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository)
    {
        _evidenceRepository = evidenceRepository;
    }

    public async Task<Evidence> Handle(CreateEvidenceCommand request, CancellationToken cancellationToken)
    {
        List<MediaItem> mediaItems = new();

        foreach (var mediaItem in request.MediaItems)
        {
            // TODO: File hosting operation
            mediaItems.Add(MediaItem.Create(mediaItem.Type, "url from file"));
        }

        Evidence evidence = Evidence.Create(
            request.WitnessId,
            request.ReportId,
            request.Caption,
            request.Description,
            request.Location,
            mediaItems
        );

        return await _evidenceRepository.AddAsync(evidence, cancellationToken);
    }
}
