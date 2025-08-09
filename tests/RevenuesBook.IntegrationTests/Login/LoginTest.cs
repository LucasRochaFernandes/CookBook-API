using CommonTestUtilities.Requests;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Exceptions;
using RevenuesBook.IntegrationTests.InlineData;
using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RevenuesBook.IntegrationTests.Login;
public class LoginTest : IClassFixture<CustomWebApplicationFactory>
{
    public readonly string method = "login";
    private readonly HttpClient _httpClient;
    private readonly string _userDefaultEmail;
    private readonly string _userDefaultPassword;
    public LoginTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _userDefaultEmail = factory.GetUserDefaultEmail();
        _userDefaultPassword = factory.GetUserDefaultPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new LoginRequest
        {
            Email = _userDefaultEmail,
            Password = _userDefaultPassword
        };
        var response = await _httpClient.PostAsJsonAsync(method, request);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Login_Invalid(string culture)
    {
        var request = LoginRequestBuilder.Build();
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
        {
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");
        }

        _httpClient.DefaultRequestHeaders.Add("Accept-Language", culture);

        var response = await _httpClient.PostAsJsonAsync(method, request);

        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);

        await using var responseBodyStream = await response.Content.ReadAsStreamAsync();
        var responseData = await JsonDocument.ParseAsync(responseBodyStream);
        var resultErrors = responseData.RootElement.GetProperty("errors").EnumerateArray()
            .Select(err => err.GetString()).ToList();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));

        Assert.Contains(expectedMessage, resultErrors);
    }
}