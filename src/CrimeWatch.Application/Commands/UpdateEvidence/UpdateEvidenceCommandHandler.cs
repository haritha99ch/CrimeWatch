﻿using Azure.Storage.Blobs.Specialized;
using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Newtonsoft.Json;

namespace CrimeWatch.Application.Commands.UpdateEvidence;
internal class UpdateEvidenceCommandHandler : IRequestHandler<UpdateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;
    private readonly IRepository<MediaItem, MediaItemId> _mediaItemRepository;
    private readonly IFileStorageService _fileStorageService;

    public UpdateEvidenceCommandHandler(
        IRepository<Evidence, EvidenceId> evidenceRepository,
        IRepository<MediaItem, MediaItemId> mediaItemRepository,
        IFileStorageService fileStorageService)
    {
        _evidenceRepository = evidenceRepository;
        _mediaItemRepository = mediaItemRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Evidence> Handle(UpdateEvidenceCommand request, CancellationToken cancellationToken)
    {
        List<MediaItem> MediaItems = JsonConvert.DeserializeObject<List<MediaItem>>(request.MediaItems ?? string.Empty) ?? new();

        Evidence evidence =
            await _evidenceRepository.GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Evidence not found");

        List<MediaItem> newMediaItems = new();
        Dictionary<BlockBlobClient, List<string>> clients = new();
        foreach (var mediaItem in request.NewMediaItems ?? new())
        {
            var (newMediaItem, blockBlobClient, blockIds)
                = await _fileStorageService.SaveFileAsync(mediaItem, evidence.WitnessId, cancellationToken);

            newMediaItems.Add(newMediaItem);
            clients.Add(blockBlobClient, blockIds);
        }

        bool hasNewMediaItems = newMediaItems.Any();

        var existingMediaItemIds = MediaItems?.Select(e => e.Id).ToList();

        bool noOfMediaItemsChanged = !evidence.MediaItems.Count.Equals(existingMediaItemIds?.Count);

        evidence.Update(
            request.Title,
            request.Description,
            request.Location,
            MediaItems
        );

        if (!noOfMediaItemsChanged && !hasNewMediaItems)
            return await _evidenceRepository.UpdateAsync(evidence);

        evidence =
                await _evidenceRepository.AsTracking().GetEvidenceWithMediaItemsByIdAsync(request.Id, cancellationToken)
                ?? throw new Exception("Evidence not found");

        if (noOfMediaItemsChanged)
        {
            var itemsToRemove = evidence.RemoveMediaItemByExistingItems(existingMediaItemIds ?? new());
            if (!hasNewMediaItems)
            {
                var result = await _evidenceRepository.UpdateAsync(evidence, cancellationToken);

                await _mediaItemRepository.RemoveRangeAsync(itemsToRemove, cancellationToken);
                await RemoveItemsFromStorage(evidence, itemsToRemove, cancellationToken);

                return result;
            }
            await _mediaItemRepository.RemoveRangeAsync(itemsToRemove, cancellationToken);

            await RemoveItemsFromStorage(evidence, itemsToRemove, cancellationToken);
        }
        if (hasNewMediaItems)
        {
            foreach (var item in newMediaItems)
            {
                evidence.AddMediaItem(item);
            }
            var result = await _evidenceRepository.UpdateAsync(evidence, cancellationToken);

            foreach (var (client, blockIds) in clients)
            {
                await client.CommitBlockListAsync(blockIds);
            }
            return result;
        }

        return await _evidenceRepository.UpdateAsync(evidence, cancellationToken);
    }

    private async Task RemoveItemsFromStorage(Evidence evidence, List<MediaItem> itemsToRemove, CancellationToken cancellationToken)
    {
        foreach (var item in itemsToRemove)
        {
            await _fileStorageService.DeleteFileByUrlAsync(item.Url, evidence.WitnessId, cancellationToken);
        }
    }
}
