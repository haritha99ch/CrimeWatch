﻿using Azure.Storage.Blobs.Specialized;
using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;

namespace CrimeWatch.Application.Commands.AddEvidenceToReport;
internal class CreateEvidenceCommandHandler : IRequestHandler<CreateEvidenceCommand, Evidence>
{
    private readonly IRepository<Evidence, EvidenceId> _evidenceRepository;
    private readonly IFileStorageService _fileStorageService;

    public CreateEvidenceCommandHandler(
        IRepository<Evidence, EvidenceId> evidenceRepository,
        IFileStorageService fileStorageService)
    {
        _evidenceRepository = evidenceRepository;
        _fileStorageService = fileStorageService;
    }

    public async Task<Evidence> Handle(CreateEvidenceCommand request, CancellationToken cancellationToken)
    {
        List<MediaItem> mediaItems = new();
        Dictionary<BlockBlobClient, List<string>> clients = new();
        foreach (var mediaItem in request.MediaItems)
        {
            var (newMediaItem, blockBlobClient, blockIds)
                = await _fileStorageService.SaveFileAsync(mediaItem, request.WitnessId, cancellationToken);

            mediaItems.Add(newMediaItem);
            clients.Add(blockBlobClient, blockIds);
        }

        Evidence evidence = Evidence.Create(
            request.WitnessId,
            request.ReportId,
            request.Caption,
            request.Description,
            request.Location,
            mediaItems
        );

        var result = await _evidenceRepository.AddAsync(evidence, cancellationToken);

        foreach (var (client, blockIds) in clients)
        {
            await client.CommitBlockListAsync(blockIds);
        }

        return result;
    }
}
