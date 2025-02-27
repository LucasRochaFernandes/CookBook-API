using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Domain.IRepositories;
using RevenuesBook.Domain.Security.Tokens;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;

namespace RevenuesBook.API.Filters;

public class AuthenticatedUserFilter : IAsyncAuthorizationFilter
{
    private readonly IAccessTokenValidator _accessTokenValidator;
    private readonly IUserRepository _userRepository;

    public AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserRepository userRepository)
    {
        _accessTokenValidator = accessTokenValidator;
        _userRepository = userRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = TokenOnRequest(context);
            var userId = _accessTokenValidator.ValidateAndGetUserId(token);
            var user = await _userRepository.FindBy(user => user.Id.Equals(userId), true);
            if (user is null)
            {
                throw new AppException(ResourceMessagesException.USER_WITHOUT_ACCESS_PERMISSION);
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse("Token is expired")
            {
                isTokenExpired = true
            });
        }
        catch (AppException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ErrorResponse(ResourceMessagesException.USER_WITHOUT_ACCESS_PERMISSION));
        }
    }

    private string TokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(authentication))
        {
            throw new AppException(ResourceMessagesException.NO_TOKEN);
        }
        return authentication["Bearer ".Length..].Trim();
    }
}
