using Microsoft.AspNetCore.Mvc;
using CookBook.API.Attributes;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Communication.Requests;

namespace CookBook.API.Controllers;

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
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(
        [FromServices] IGetRecipeByIdUseCase useCase,
        [FromRoute] Guid id
        )
    {
        var result = await useCase.Execute(id);
        return Ok(result);
    }


}
