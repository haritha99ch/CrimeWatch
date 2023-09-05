using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Microsoft.AspNetCore.Http;

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
        BlobContainerClient clientContainer = _blobServiceClient.GetBlobContainerClient(witnessId.Value.ToString());
        Uri uri = new(url);
        string fileName = Path.GetFileName(uri.LocalPath);
        BlobClient blobClient = clientContainer.GetBlobClient(fileName);
        return await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    public async Task<(MediaItem, BlockBlobClient, List<string>)> SaveFileAsync(IFormFile file, WitnessId witnessId, CancellationToken cancellationToken)
    {
        BlobContainerClient clientContainer = _blobServiceClient.GetBlobContainerClient(witnessId.Value.ToString());

        await clientContainer.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

        string fileExtension = Path.GetExtension(file.FileName);
        string fileName = $"{Guid.NewGuid()}{fileExtension}";

        MediaItemType mediaItemType = file.ContentType switch
        {
            "image/jpeg" => MediaItemType.Image,
            "image/png" => MediaItemType.Image,
            "video/mp4" => MediaItemType.Video,
            _ => throw new ArgumentException("Invalid file type")
        };

        BlockBlobClient blockBlobClient = clientContainer.GetBlockBlobClient(fileName);

        // Upload the file in blocks
        List<string> blockIds = new();
        using (Stream stream = file.OpenReadStream())
        {
            int blockSize = 256 * 1024;
            byte[] buffer = new byte[blockSize];
            int bytesRead;
            while ((bytesRead = await stream.ReadAsync(buffer.AsMemory(0, blockSize), cancellationToken)) > 0)
            {
                cancellationToken.ThrowIfCancellationRequested();

                string blockId = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                blockIds.Add(blockId);
                using MemoryStream memoryStream = new(buffer, 0, bytesRead);
                await blockBlobClient.StageBlockAsync(blockId, memoryStream, cancellationToken: cancellationToken);
            }
        }

        // Get url
        string url = blockBlobClient.Uri.AbsoluteUri;

        return (MediaItem.Create(mediaItemType, url), blockBlobClient, blockIds);
    }
}
