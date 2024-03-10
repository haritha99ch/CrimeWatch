using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Infrastructure.Contracts.Services;
using Infrastructure.Types.Files;

namespace Infrastructure.Services;
public class BlobStorageClient : IBlobStorageClient
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobStorageClient(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task<FileUpload> UploadFileAsync(
            string containerName,
            string fileName,
            Stream fileStream,
            CancellationToken cancellationToken
        )
    {
        var clientContainer = _blobServiceClient.GetBlobContainerClient(containerName);
        await clientContainer.CreateIfNotExistsAsync(
            PublicAccessType.Blob,
            cancellationToken: cancellationToken);

        var blobClient = clientContainer.GetBlobClient(fileName);
        await blobClient.UploadAsync(fileStream, true, cancellationToken);
        return new(blobClient.Name, blobClient.Uri.AbsoluteUri);
    }

    public async Task<bool> DeleteFileByUriAsync(
            string containerName,
            string fileName,
            CancellationToken cancellationToken
        )
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        try
        {
            await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }

    public async Task<bool> DeleteContainerAsync(
            string containerName,
            CancellationToken cancellationToken
        )
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        try
        {
            await containerClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
        return true;
    }
}
