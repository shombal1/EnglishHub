using EnglishHub.Forums.Domain.Authorization;
using EnglishHub.Forums.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace EnglishHub.Forums.Api.Middleware;

public class ErrorHandingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        ProblemDetailsFactory problemDetailsFactory,
        ILogger<ErrorHandingMiddleware> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) 
        {
            ProblemDetails problemDetails;
            switch (exception)
            {
                case IntentionManagerException intentionManagerException:
                    problemDetails = problemDetailsFactory.CreateFrom(context,intentionManagerException);
                    break;
                case ValidationException validationException:
                    problemDetails = problemDetailsFactory.CreateFrom(context, validationException);
                    break;
                case DomainException domainException:
                    problemDetails = problemDetailsFactory.CreateFrom(context, domainException);

                    logger.LogError(domainException, "domain exception");
                    break;
                default:
                    problemDetails = problemDetailsFactory.CreateProblemDetails(
                        context,
                        StatusCodes.Status500InternalServerError,
                        "Unhandled error",
                        detail: exception.Message);

                    logger.LogError(exception, "Unhandled exception");
                    break;
            }

            context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync(problemDetails, problemDetails.GetType());
        }
    }
}