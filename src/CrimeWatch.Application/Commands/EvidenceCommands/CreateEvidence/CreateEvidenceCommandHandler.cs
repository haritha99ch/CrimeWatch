using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Commands.EvidenceCommands.CreateEvidence;
internal class CreateEvidenceCommandHandler(
    IRepository<Evidence, EvidenceId> evidenceRepository,
    IFileStorageService fileStorageService) : IRequestHandler<CreateEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(CreateEvidenceCommand request, CancellationToken cancellationToken)
    {
        List<MediaItem> mediaItems = new();
        foreach (var mediaItem in request.MediaItems)
        {
            var newMediaItem = await fileStorageService.SaveFileAsync(mediaItem, request.WitnessId, cancellationToken);

            mediaItems.Add(newMediaItem);
        }

        var evidence = Evidence.Create(
            request.WitnessId,
            request.ReportId,
            request.Caption,
            request.Description,
            request.Location,
            mediaItems
        );

        var result = await evidenceRepository.AddAsync(evidence, cancellationToken);

        return result;
    }
}
