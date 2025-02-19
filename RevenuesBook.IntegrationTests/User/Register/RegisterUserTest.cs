using CommonTestUtilities.Requests;
using RevenuesBook.Exceptions;
using RevenuesBook.IntegrationTests.InlineData;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RevenuesBook.IntegrationTests.User.Register;
public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient;
    public RegisterUserTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Success()
    {
        var request = RegisterUserRequestBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("User", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        await using var responseBodyStream = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBodyStream);

        var resultUserId = responseData.RootElement.GetProperty("userId").GetString();
        Assert.NotNull(resultUserId);
    }
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RegisterUserRequestBuilder.Build();
        request.Name = string.Empty;

        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
        }

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _httpClient.PostAsJsonAsync("User", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var responseBodyStream = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBodyStream);
        var resultErrors = responseData.RootElement.GetProperty("errors").EnumerateArray()
            .Select(err => err.GetString()).ToList();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));

        Assert.Contains(expectedMessage, resultErrors);
    }
}
