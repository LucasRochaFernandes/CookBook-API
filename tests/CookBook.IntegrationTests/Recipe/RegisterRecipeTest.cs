using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using System.Net;
using System.Net.Http.Headers;

namespace CookBook.IntegrationTests.Recipe;
public class RegisterRecipeTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string METHOD = "recipe";
    private readonly HttpClient _httpClient;
    private readonly Guid _userIdentifier;

    public RegisterRecipeTest(CustomWebApplicationFactory factory)
    {
        _httpClient = factory.CreateClient();
        _userIdentifier = factory.GetUserDefaultId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RegisterRecipeFormDataRequestBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Clear();
        _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en"));

        var multipartContent = new MultipartFormDataContent();
        var requestProperties = request.GetType().GetProperties().ToList();
        foreach (var property in requestProperties)
        {
            var propertyValue = property.GetValue(request);

            if (string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                continue;

            if (propertyValue is System.Collections.IList list)
            {
                AddListToMultipartContent(multipartContent, property.Name, list);
            }
            else
            {
                multipartContent.Add(new StringContent(propertyValue.ToString()!), property.Name);
            }
        }

        var response = await _httpClient.PostAsync(METHOD, multipartContent);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

    }

    private static void AddListToMultipartContent(
        MultipartFormDataContent multipartContent,
        string propertyName,
        System.Collections.IList list)
    {
        var itemType = list.GetType().GetGenericArguments().Single();

        if (itemType.IsClass && itemType != typeof(string))
        {
            AddClassListToMultipartContent(multipartContent, propertyName, list);
        }
        else
        {
            foreach (var item in list)
            {
                multipartContent.Add(new StringContent(item.ToString()!), propertyName);
            }
        }
    }
    private static void AddClassListToMultipartContent(
        MultipartFormDataContent multipartContent,
        string propertyName,
        System.Collections.IList list)
    {
        var index = 0;

        foreach (var item in list)
        {
            var classPropertiesInfo = item.GetType().GetProperties().ToList();

            foreach (var prop in classPropertiesInfo)
            {
                var value = prop.GetValue(item, null);
                multipartContent.Add(new StringContent(value!.ToString()!), $"{propertyName}[{index}][{prop.Name}]");
            }

            index++;
        }
    }
}
