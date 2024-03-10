using Infrastructure.Types.Files;

namespace Infrastructure.Contracts.Services;
public interface IBlobStorageClient
{
    /// <summary>
    ///     Upload the file steam.
    /// </summary>
    /// <param name="containerName">Folder/ Container name</param>
    /// <param name="fileName">File name upload as.</param>
    /// <param name="fileStream">Memory stream</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Url to the file</returns>
    Task<FileUpload> UploadFileAsync(
            string containerName,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteFileByUriAsync(
            string containerName,
            string fileName,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken);
}
