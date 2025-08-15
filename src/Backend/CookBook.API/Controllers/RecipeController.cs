using CookBook.API.Attributes;
using CookBook.Application.UseCases.Recipes.Interfaces;
using CookBook.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers;

[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class RecipeController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterRecipeUseCase useCase,
        [FromForm] RegisterRecipeFormDataRequest request
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
    [HttpDelete]
    [Route("{id}")]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteRecipeUseCase useCase,
        [FromRoute] Guid id
        )
    {
        await useCase.Execute(id);
        return NoContent();
    }
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> Update(
        [FromServices] IUpdateRecipeUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] RecipeRequest request
        )
    {
        await useCase.Execute(id, request);
        return NoContent();
    }
    [HttpPost("generate")]
    public async Task<IActionResult> Generate(
        [FromServices] IGenerateRecipeUseCase useCase,
        [FromBody] GenerateRecipeRequest request
        )
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }
    [HttpPut]
    [Route("image/{id}")]
    public async Task<IActionResult> UpdateImage(
        [FromServices] IAddUpdateImageCoverUseCase useCase,
        [FromRoute] Guid id,
        IFormFile file
        )
    {
        await useCase.Execute(id, file);
        return NoContent();
    }
}
