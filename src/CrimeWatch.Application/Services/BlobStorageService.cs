﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CrimeWatch.Application.Contracts.Services;

namespace CrimeWatch.Application.Services;
internal class BlobStorageService : IFileStorageService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<bool> DeleteFileByUrlAsync(string url, WitnessId witnessId, CancellationToken cancellationToken)
    {
        var clientContainer = _blobServiceClient.GetBlobContainerClient(witnessId.Value.ToString());
        Uri uri = new(url);
        var fileName = Path.GetFileName(uri.LocalPath);
        var blobClient = clientContainer.GetBlobClient(fileName);
        return await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<MediaItem> SaveFileAsync(IFormFile file, WitnessId witnessId, CancellationToken cancellationToken)
    {
        var clientContainer = _blobServiceClient.GetBlobContainerClient(witnessId.Value.ToString());

        await clientContainer.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";

        var mediaItemType = file.ContentType switch
        {
            "image/jpeg" => MediaItemType.Image,
            "image/png" => MediaItemType.Image,
            "video/mp4" => MediaItemType.Video,
            _ => throw new ArgumentException("Invalid file type")
        };

        // Save file to blob storage and get url
        var blockBlobClient = clientContainer.GetBlockBlobClient(fileName);
        await blockBlobClient.UploadAsync(file.OpenReadStream(), cancellationToken: cancellationToken);
        var url = blockBlobClient.Uri.AbsoluteUri;

        return MediaItem.Create(mediaItemType, url) ?? throw new($"Could not upload {fileName}.{fileExtension}");
    }
}
