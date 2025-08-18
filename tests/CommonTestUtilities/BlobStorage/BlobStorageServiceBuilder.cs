using Bogus;
using CookBook.Domain.Entities;
using CookBook.Domain.Services.Storage;
using Moq;

namespace CommonTestUtilities.BlobStorage;
public class BlobStorageServiceBuilder
{
    private readonly Mock<IBlobStorageService> _mock;

    public BlobStorageServiceBuilder()
    {
        _mock = new Mock<IBlobStorageService>();
    }

    public BlobStorageServiceBuilder GetFileUrl(User user, string? filename)
    {
        if (string.IsNullOrEmpty(filename))
        {
            return this;
        }
        var faker = new Faker();
        var imageUrl = faker.Image.PlaceImgUrl();
        _mock.Setup(blobStorage => blobStorage.GetFileUrl(user, filename)).ReturnsAsync(imageUrl);
        return this;
    }
    public BlobStorageServiceBuilder GetFileUrl(User user, IList<Recipe> recipes)
    {
        foreach (var recipe in recipes)
        {
            GetFileUrl(user, recipe.ImageIdentifier);
        }
        return this;
    }
    public IBlobStorageService Build()
    {
        return _mock.Object;
    }


}
