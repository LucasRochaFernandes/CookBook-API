using CookBook.Domain.Entities;

namespace CookBook.Domain.Services.Storage;
public interface IBlobStorageService
{
    public Task Upload(User user, Stream file, string filename);
    public Task<string> GetFileUrl(User user, string filename);
    public Task Delete(User user, string filename);
}
