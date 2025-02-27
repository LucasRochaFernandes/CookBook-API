using Microsoft.AspNetCore.Mvc;
using RevenuesBook.Application.UseCases.Login.Interfaces;
using RevenuesBook.Communication.Requests;

namespace RevenuesBook.API.Controllers;

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
