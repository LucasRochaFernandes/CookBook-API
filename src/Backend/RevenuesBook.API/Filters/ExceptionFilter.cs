using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RevenuesBook.Communication.Responses;
using RevenuesBook.Exceptions;
using RevenuesBook.Exceptions.ExceptionsBase;
using System.Net;

namespace RevenuesBook.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AppException)
        {
            HandleAppException(context);
        }
        else
        {
            HandleUnknownException(context);
        }
    }

    private static void HandleAppException(ExceptionContext context)
    {
        if (context.Exception is UnauthorizedException)
        {
            var exception = (UnauthorizedException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new UnauthorizedObjectResult(
                new ErrorResponse(exception.Message));
        }
        if (context.Exception is ValidationException)
        {
            var exception = (ValidationException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(
                new ErrorResponse(exception.Errors));
        }
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(ResourceMessagesException.UNKNOWN_ERROR));
    }
}
