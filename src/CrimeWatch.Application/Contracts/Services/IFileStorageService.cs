using Azure.Storage.Blobs.Specialized;
using CrimeWatch.Domain.AggregateModels.ReportAggregate;
using Microsoft.AspNetCore.Http;

namespace CrimeWatch.Application.Contracts.Services;
internal interface IFileStorageService
{
    Task<(MediaItem, BlockBlobClient, List<string>)> SaveFileAsync(IFormFile file, WitnessId witnessId, CancellationToken cancellationToken); // Files are stored as ClientId/some-guid.xxx
    Task<bool> DeleteFileByUrlAsync(string url, WitnessId witnessId, CancellationToken cancellationToken);
}
