using Microsoft.AspNetCore.Mvc;
using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Communication.Requests;

namespace CookBook.API.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] LoginRequest request)
    {
        var result = await useCase.Execute(request);
        return Ok(result);
    }
}
