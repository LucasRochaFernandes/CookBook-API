using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RevenuesBook.IntegrationTests.User.Update;
public class UpdateUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly string METHOD = "user";
    private readonly Guid _userDefaultId;

    public UpdateUserTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _userDefaultId = factory.GetUserDefaultId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userDefaultId);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var request = UpdateUserRequestBuilder.Build();

        var response = await _httpClient.PutAsJsonAsync(METHOD, request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
