using CrimeWatch.Application.Contracts.Services;
using Newtonsoft.Json;

namespace CrimeWatch.Application.Commands.EvidenceCommands.UpdateEvidence;
internal class UpdateEvidenceCommandHandler(IRepository<Evidence, EvidenceId> evidenceRepository,
        IRepository<MediaItem, MediaItemId> mediaItemRepository,
        IFileStorageService fileStorageService)
    : IRequestHandler<UpdateEvidenceCommand, Evidence>
{

    public async Task<Evidence> Handle(UpdateEvidenceCommand request, CancellationToken cancellationToken)
    {
        var MediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(request.MediaItems ?? string.Empty) ?? new();

        var evidence =
            await evidenceRepository.GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
            ?? throw new("Evidence not found");

        List<MediaItem> newMediaItems = new();
        foreach (var mediaItem in request.NewMediaItems ?? new())
        {
            var newMediaItem =
                await fileStorageService.SaveFileAsync(mediaItem, evidence.WitnessId, cancellationToken);

            newMediaItems.Add(newMediaItem);
        }

        var hasNewMediaItems = newMediaItems.Any();

        var existingMediaItemIds = MediaItems?.Select(e => e.Id).ToList();

        var noOfMediaItemsChanged = !evidence.MediaItems.Count.Equals(existingMediaItemIds?.Count);

        evidence.Update(
            request.Title,
            request.Description,
            request.Location,
            MediaItems
        );

        if (!noOfMediaItemsChanged && !hasNewMediaItems)
            return await evidenceRepository.UpdateAsync(evidence);

        await evidenceRepository.UpdateAsync(evidence);

        evidence =
            await evidenceRepository.AsTracking().GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
            ?? throw new("Evidence not found");

        if (noOfMediaItemsChanged)
        {
            var itemsToRemove = evidence.RemoveMediaItemByExistingItems(existingMediaItemIds ?? new());
            if (!hasNewMediaItems)
            {
                var result = await evidenceRepository.UpdateAsync(evidence, cancellationToken);

                await mediaItemRepository.RemoveRangeAsync(itemsToRemove, cancellationToken);
                await RemoveItemsFromStorage(evidence, itemsToRemove, cancellationToken);

                return result;
            }
            await mediaItemRepository.RemoveRangeAsync(itemsToRemove, cancellationToken);

            await RemoveItemsFromStorage(evidence, itemsToRemove, cancellationToken);
        }
        if (hasNewMediaItems)
        {
            foreach (var item in newMediaItems)
            {
                evidence.AddMediaItem(item);
            }
            return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
        }

        return await evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }

    private async Task RemoveItemsFromStorage(Evidence evidence, List<MediaItem> itemsToRemove,
        CancellationToken cancellationToken)
    {
        foreach (var item in itemsToRemove)
        {
            await fileStorageService.DeleteFileByUrlAsync(item.Url, evidence.WitnessId, cancellationToken);
        }
    }
}
