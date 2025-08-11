using CookBook.Application.UseCases.Dashboard.Interfaces;
using CookBook.Application.UseCases.Recipes.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DashboardController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromServices] IGetDashboardUseCase useCase)
    {
        var result = await useCase.Execute();

        if (!result.Recipes.Any())
            return NoContent();

        return Ok(result);
    }
}
