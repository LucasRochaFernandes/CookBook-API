using CookBook.Application.UseCases.Login.Interfaces;
using CookBook.Communication.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpGet]
    [Route("google")]
    public async Task<IActionResult> LoginGoogle(string returnUrl,
        [FromServices] IExternalLoginUseCase useCase)
    {
        var authenticate = await Request.HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (IsNotAuthenticated(authenticate))
        {
            return Challenge(GoogleDefaults.AuthenticationScheme);
        }
        else
        {
            var claims = authenticate.Principal!.Identities.First().Claims;
            var name = claims.First(c => c.Type == ClaimTypes.Name).Value;
            var email = claims.First(c => c.Type == ClaimTypes.Email).Value;
            var token = await useCase.Execute(name, email);
            return Redirect($"{returnUrl}/{token}");
        }
    }

    [HttpGet]
    [Route("code-reset-password/{email}")]
    public async Task<IActionResult> RequestCodeResetPassword(
        [FromServices] IRequestCodeResetPasswordUseCase useCase,
        [FromRoute] string email
        )
    {
        await useCase.Execute(email);
        return Accepted();
    }

    [HttpPut]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] ResetYourPasswordRequest request,
        [FromServices] IResetPasswordUseCase useCase
        )
    {
        await useCase.Execute(request);
        return NoContent();
    }
    protected static bool IsNotAuthenticated(AuthenticateResult authenticate)
    {
        return authenticate.Succeeded is false
            || authenticate.Principal is null
            || authenticate.Principal.Identities.Any(id => id.IsAuthenticated) is false;
    }
}
