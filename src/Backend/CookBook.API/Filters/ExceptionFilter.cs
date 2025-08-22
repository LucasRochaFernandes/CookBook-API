using CookBook.Communication.Responses;
using CookBook.Exceptions;
using CookBook.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CookBook.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is AppException appException)
        {
            HandleAppException(appException, context);
        }
        else
        {
            HandleUnknownException(context);
        }
    }

    private static void HandleAppException(AppException appException, ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)appException.GetHttpStatusCode();
        context.Result = new ObjectResult(new ErrorResponse(appException.GetErrorMessages()));
    }
    private static void HandleUnknownException(ExceptionContext context)
    {
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(ResourceMessagesException.UNKNOWN_ERROR));
    }
}
