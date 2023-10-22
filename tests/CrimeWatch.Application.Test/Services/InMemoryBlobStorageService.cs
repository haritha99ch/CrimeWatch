using CrimeWatch.Application.Contracts.Services;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using CrimeWatch.Domain.Enums;
using CrimeWatch.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Test.Services;
internal class InMemoryBlobStorageService : IFileStorageService
{
    private readonly Dictionary<string, MemoryStream> _storage = new();

    public Task<bool> DeleteFileByUrlAsync(string url, WitnessId witnessId, CancellationToken cancellationToken)
    {
        if (_storage.ContainsKey(url))
        {
            _storage.Remove(url);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public async Task<MediaItem> SaveFileAsync(IFormFile file, WitnessId witnessId, CancellationToken cancellationToken)
    {
        var fileExtension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{fileExtension}";

        var mediaItemType = file.ContentType switch
        {
            "image/jpeg" => MediaItemType.Image,
            "image/png" => MediaItemType.Image,
            "video/mp4" => MediaItemType.Video,
            _ => throw new ArgumentException("Invalid file type")
        };

        MemoryStream memoryStream = new();
        await file.CopyToAsync(memoryStream, cancellationToken);
        _storage.Add(fileName, memoryStream);

        return MediaItem.Create(mediaItemType, fileName)!;
    }
}
