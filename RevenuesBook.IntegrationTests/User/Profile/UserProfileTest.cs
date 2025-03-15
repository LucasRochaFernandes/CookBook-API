using CommonTestUtilities.Tokens;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
namespace RevenuesBook.IntegrationTests.User.Profile;
public class UserProfileTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly string METHOD = "user";
    private readonly string _userDefaultEmail;
    private readonly Guid _userDefaultId;
    public UserProfileTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _userDefaultEmail = factory.GetUserDefaultEmail();
        _userDefaultId = factory.GetUserDefaultId();
    }
    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userDefaultId);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.GetAsync(METHOD);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await using var responseBodyStream = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBodyStream);
        var resultName = responseData.RootElement.GetProperty("email").GetString();
        Assert.Equal(_userDefaultEmail, resultName);
    }
}
