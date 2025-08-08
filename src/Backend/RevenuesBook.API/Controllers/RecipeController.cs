using Microsoft.AspNetCore.Mvc;
using RevenuesBook.API.Attributes;
using RevenuesBook.Application.UseCases.Recipes.Interfaces;
using RevenuesBook.Communication.Requests;

namespace RevenuesBook.API.Controllers;

[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class RecipeController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterRecipeUseCase useCase,
        [FromBody] RecipeRequest request
        )
    {
        var result = await useCase.Execute(request);
        return Created(string.Empty, result);
    }
    [HttpPost("filter")]
    public async Task<IActionResult> Filter(
        [FromServices] IFilterRecipeUseCase useCase,
        [FromBody] RecipeFilterRequest request)
    {
        var result = await useCase.Execute(request);
        if (result.Recipes.Any())
        {
            return Ok(result);
        }
        return NoContent();
    }
}
