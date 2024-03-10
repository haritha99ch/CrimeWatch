using Domain.Common.Results;
using Microsoft.AspNetCore.Http;

namespace Persistence.Contracts.Services;
public interface IFileStorageService
{
    Task<MediaItemUploadResult> UploadFileAsync(
            string containerName,
            IFormFile file,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteFileAsync(
            string containerName,
            string fileName,
            CancellationToken cancellationToken
        );

    Task<bool> DeleteContainerAsync(string containerName, CancellationToken cancellationToken);
}
