namespace CrimeWatch.Application.Contracts.Services;
public interface IFileStorageService
{
    Task<MediaItem> SaveFileAsync(IFormFile file, WitnessId witnessId,
        CancellationToken cancellationToken); // Files are stored as ClientId/some-guid.xxx

    Task<bool> DeleteFileByUrlAsync(string url, WitnessId witnessId, CancellationToken cancellationToken);
}
