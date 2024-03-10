using Domain.AggregateModels.ReportAggregate.Enums;
using Domain.Common.Models;
using Domain.Common.Results;
using Infrastructure.Contracts.Services;
using Microsoft.AspNetCore.Http;
using Persistence.Contracts.Services;

namespace Application.Test.Service;
internal class AzureBlobStorageTestService : IFileStorageService
{
    private readonly IBlobStorageClient _blobStorageClient;
    private const string DefaultTestContainerName = "defaulttestcontainer";

    public AzureBlobStorageTestService(IBlobStorageClient blobStorageClient)
    {
        _blobStorageClient = blobStorageClient;
    }

    public async Task<MediaItemUploadResult> UploadFileAsync(
            string containerName,
            IFormFile file,
            CancellationToken cancellationToken
        )
    {
        try
        {
            var stream = new MemoryStream();
            await file.CopyToAsync(stream, cancellationToken);
            stream.Position = 0;
            var fileUpload = await _blobStorageClient
                .UploadFileAsync(DefaultTestContainerName,
                    $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}",
                    stream,
                    cancellationToken);
            var fileType = file.ContentType.ToLower() switch
            {
                "audio/mpeg" or "audio/wav" => MediaType.Audio,
                "video/mp4" or "video/x-msvideo" => MediaType.Video,
                "image/jpeg" or "image/png" => MediaType.Image,
                "application/pdf" or "application/msword"
                    or "application/vnd.openxmlformats-officedocument.wordprocessingml.document" => MediaType.Document,
                _ => throw new ArgumentException("Unsupported media type")
            };
            return MediaUpload.Create(fileUpload.FileName, fileUpload.Url, fileType);
        }
        catch (Exception e)
        {
            return e;
        }
    }
    public async Task<bool> DeleteFileAsync(string containerName, string fileName, CancellationToken cancellationToken)
        => await _blobStorageClient.DeleteFileByUriAsync(DefaultTestContainerName, fileName, cancellationToken);

    public async Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken)
        => await _blobStorageClient.DeleteContainerAsync(DefaultTestContainerName, cancellationToken);
}
