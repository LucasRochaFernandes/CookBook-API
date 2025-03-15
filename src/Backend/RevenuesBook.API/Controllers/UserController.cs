using Microsoft.AspNetCore.Mvc;
using RevenuesBook.API.Attributes;
using RevenuesBook.Application.UseCases.User.Interfaces;
using RevenuesBook.Communication.Requests;
using RevenuesBook.Communication.Responses;

namespace RevenuesBook.API.Controllers;


[Route("[controller]")]
[ApiController]
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
    [HttpGet]
    [AuthenticatedUser]
    public async Task<IActionResult> UserProfile(
        [FromServices] IUserProfileUseCase useCase
        )
    {
        var result = await useCase.Execute();
        return Ok(result);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> Update(
        [FromBody] UpdateUserRequest body,
        [FromServices] IUpdateUserUseCase useCase
        )
    {
        await useCase.Execute(body);
        return NoContent();
    }
    [HttpPut("change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [AuthenticatedUser]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequest body,
        [FromServices] IChangePasswordUseCase useCase
        )
    {
        await useCase.Execute(body);
        return NoContent();
    }
}
