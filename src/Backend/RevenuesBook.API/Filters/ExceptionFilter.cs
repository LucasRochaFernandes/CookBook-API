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

    private void HandleAppException(ExceptionContext context)
    {
        if (context.Exception is ValidationException)
        {
            var exception = (ValidationException)context.Exception;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = new BadRequestObjectResult(new ValidationExceptionResponse(exception.Errors));
        }
    }

    private void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ValidationExceptionResponse(
            [ResourceMessagesException.UNKNOWN_ERROR]
            ));
    }
}
