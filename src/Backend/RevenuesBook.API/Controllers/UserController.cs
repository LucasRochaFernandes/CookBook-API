using Microsoft.AspNetCore.Mvc;
using RevenuesBook.API.Attributes;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.API.Controllers;


[Route("[controller]")]
[ApiController]
[AuthenticatedUser]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RegisterUserResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest body,
        [FromServices] IRegisterUserUseCase useCase
        )
    {
        var result = await useCase.Execute(body);
        return Created(string.Empty, result);
    }
}
