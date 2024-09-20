using EnglishHub.Forums.Domain.Authorization;
using EnglishHub.Forums.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EnglishHub.Forums.Api.Middleware;

public static class ProblemDetailsFactoryExtension
{
    public static ProblemDetails CreateFrom(this ProblemDetailsFactory problemDetailsFactory, HttpContext context,
        ValidationException validationException)
    {
        ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

        foreach (var error in validationException.Errors)
        {
            modelStateDictionary.AddModelError(error.PropertyName,error.ErrorMessage);
        }
        
        ProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
            context,
            modelStateDictionary,
            StatusCodes.Status400BadRequest,
            "Bad request");

        return problemDetails;
    }

    public static ProblemDetails CreateFrom(this ProblemDetailsFactory problemDetailsFactory, HttpContext context,
        DomainException domainException)
    {
        return problemDetailsFactory.CreateProblemDetails(
            context,
            domainException.ErrorCode switch
            {
                ErrorCode.Gone => StatusCodes.Status410Gone,
                _ => throw new ArgumentOutOfRangeException()
            },
            "Error",
            detail: domainException.Message);
    }

    public static ProblemDetails CreateFrom(this ProblemDetailsFactory problemDetailsFactory, HttpContext context,
        IntentionManagerException intentionManagerException)
    {
        return problemDetailsFactory.CreateProblemDetails(
            context,
            StatusCodes.Status403Forbidden,
            "Authorization failed",
            detail: intentionManagerException.Message);
    }
}