using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using CookBook.Domain.Entities;
using CookBook.Domain.Services.Storage;
using CookBook.Domain.ValueObjects;

namespace CookBook.Infra.Services.Storage;
public class AzureStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    public AzureStorageService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }

    public async Task Delete(User user, string filename)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(user.Id.ToString());
        var exists = await containerClient.ExistsAsync();
        if (exists.Value)
        {
            await containerClient.DeleteBlobIfExistsAsync(filename);
        }
    }

    public async Task DeleteContainer(Guid userId)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(userId.ToString());
        await containerClient.DeleteIfExistsAsync();
    }

    public async Task<string> GetFileUrl(User user, string filename)
    {
        var containerName = user.Id.ToString();
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        var exists = await containerClient.ExistsAsync();
        if (exists.Value is false)
        {
            return string.Empty;
        }
        var blobClient = containerClient.GetBlobClient(filename);
        exists = await blobClient.ExistsAsync();
        if (exists.Value)
        {
            var sasBuilder = new BlobSasBuilder
            {
                BlobContainerName = containerName,
                BlobName = filename,
                Resource = "b",
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(AppRuleConstants.MAXIMUM_IMAGE_URL_LIFETIME_IN_MINUTES),
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            return blobClient.GenerateSasUri(sasBuilder).ToString();
        }
        return string.Empty;
    }

    public async Task Upload(User user, Stream file, string filename)
    {
        var blobContainerClient = _blobServiceClient.GetBlobContainerClient(user.Id.ToString());
        await blobContainerClient.CreateIfNotExistsAsync();

        var blobClient = blobContainerClient.GetBlobClient(filename);

        await blobClient.UploadAsync(file, overwrite: true);
    }
}
