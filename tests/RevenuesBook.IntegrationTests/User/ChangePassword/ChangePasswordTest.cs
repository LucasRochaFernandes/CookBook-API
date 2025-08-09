using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RevenuesBook.IntegrationTests.User.ChangePassword;
public class ChangePasswordTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    private readonly Guid _userDefaultId;
    private readonly string _userDefaultPassword;
    private readonly string METHOD = "user/change-password";

    public ChangePasswordTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _userDefaultId = factory.GetUserDefaultId();
        _userDefaultPassword = factory.GetUserDefaultPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = ChangePasswordRequestBuilder.Build();
        request.CurrentPassword = _userDefaultPassword;
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userDefaultId);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PutAsJsonAsync(METHOD, request);

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

    }

}
