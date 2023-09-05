using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.UpdateEvidence;
internal class UpdateEvidenceCommandHandler : IRequestHandler<UpdateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;
    private readonly IRepository<MediaItem, MediaItemId> _mediaItemRepository;

    public UpdateEvidenceCommandHandler(
        IRepository<Evidence, EvidenceId> evidenceRepository,
        IRepository<MediaItem, MediaItemId> mediaItemRepository)
    {
        _evidenceRepository = evidenceRepository;
        _mediaItemRepository = mediaItemRepository;
    }

    public async Task<Evidence> Handle(UpdateEvidenceCommand request, CancellationToken cancellationToken)
    {
        List<MediaItem> newMediaItems = new();
        foreach (var mediaItem in request.NewMediaItems)
        {
            // TODO: Hosting action
            MediaItem item = MediaItem.Create(mediaItem.Type, "New Url");
            newMediaItems.Add(item);
        }

        bool hasNewMediaItems = newMediaItems.Any();

        var existingMediaItemIds = request.MediaItems.Select(e => e.Id).ToList();
        Evidence evidence =
            await _evidenceRepository.GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Evidence not found");

        bool noOfMediaItemsChanged = !evidence.MediaItems.Count.Equals(existingMediaItemIds.Count);

        evidence.Update(
            request.Title,
            request.Description,
            request.Location,
            request.MediaItems
        );

        if (!noOfMediaItemsChanged && !hasNewMediaItems)
            return await _evidenceRepository.UpdateAsync(evidence);

        await _evidenceRepository.UpdateAsync(evidence);

        evidence =
                await _evidenceRepository.AsTracking().GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
                ?? throw new Exception("Evidence not found");

        if (noOfMediaItemsChanged)
        {
            var itemsToRemove = evidence.RemoveMediaItemByExistingItems(existingMediaItemIds);
            if (!hasNewMediaItems)
            {
                var updatedEvidence = await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
                await _mediaItemRepository.RemoveRangeAsync(itemsToRemove, cancellationToken);
            }
        }


        foreach (var item in newMediaItems)
        {
            evidence.AddMediaItem(item);
        }

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }
}
