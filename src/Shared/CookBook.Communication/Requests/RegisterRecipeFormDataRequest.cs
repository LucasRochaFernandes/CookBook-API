using Microsoft.AspNetCore.Http;

namespace CookBook.Communication.Requests;
public class RegisterRecipeFormDataRequest : RecipeRequest
{
    public IFormFile? Image { get; set; }
}
