namespace Infrastructure.Contracts.Services;
public interface IBlobStorageClient
{
    Task<string> UploadFileAsync(
            string containerName,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteFileByUriAsync(
            string uri,
            string containerName,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken);
}
