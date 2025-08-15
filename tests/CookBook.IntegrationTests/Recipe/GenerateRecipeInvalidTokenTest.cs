using CommonTestUtilities.Requests;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace CookBook.IntegrationTests.Recipe;
public class GenerateRecipeInvalidTokenTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private const string METHOD = "recipe/generate";
    public GenerateRecipeInvalidTokenTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }
    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = GenerateRecipeRequestBuilder.Build();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "tokenInvalid");
        var response = await _httpClient.PostAsJsonAsync(METHOD, request);
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response.StatusCode);
    }

}
