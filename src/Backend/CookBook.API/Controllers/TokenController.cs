using CookBook.Application.UseCases.Token.Interfaces;
using CookBook.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CookBook.API.Controllers;
[Route("[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(
        [FromServices] IUseRefreshTokenUseCase useCase,
        [FromBody] NewTokenRequest request)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

}
