using DesafioHyperativa.API.Infra;
using DesafioHyperativa.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DesafioHyperativa.API.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    private object ExtractError(Exception exception)
    {
        var lastMenssage = GetInnermostException(exception).Message;
        var erro = new[]
        {
            lastMenssage
        };

        if (exception is BusinessException)
        {
            var ex = (BusinessException)exception;

            return new
            {
                Status = ex.ErrorCode,
                Title = ex.ErrorTitle,
                Errors = new[] {
                        exception.Data["Message"] ?? erro
                    }
            };
        }

        if (exception is CustomException)
        {
            var ex = (CustomException)exception;

            return new CustomModelError
            {
                Status = ex.ErrorCode.ToString(),
                Title = ex.ErrorTitle,
                Errors = erro
            };
        }

        return new CustomModelError
        {
            Status = "500",
            Title = "Internal error",
            Errors = new[] {
                    "An internal error occurred"
                }
        };
    }

    public void OnException(ExceptionContext context)
    {
        var errorException = ExtractError(context.Exception);
        context.ExceptionHandled = true;

        if (context.Exception is CustomException)
        {
            var ex = (CustomException)context.Exception;

            switch (ex.ErrorCode)
            {
                case 400:
                    context.Result = new BadRequestObjectResult(errorException);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case 401:
                    context.Result = new UnauthorizedObjectResult(errorException);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                case 429:
                    context.Result = new BadRequestObjectResult(errorException);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    break;

                default:
                    context.Result = new BadRequestObjectResult(errorException);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
        }
        else
        {
            context.Result = new ObjectResult(errorException)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var controllerName = context.RouteData.Values["controller"];
            var actionName = context.RouteData.Values["action"];

            var exception = GetInnermostException(context.Exception);
            _logger.LogError(exception, "Error execute {controllerName}/{actionName} - {errorMessage} ",
                controllerName, actionName, exception.Message);
        }
    }

    private static Exception GetInnermostException(Exception ex)
    {
        while (ex.InnerException != null)
        {
            ex = ex.InnerException;
        }

        return ex;
    }
}
