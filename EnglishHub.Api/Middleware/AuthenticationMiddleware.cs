using EnglishHub.Api.Authentication;
using EnglishHub.Domain.Authentication;

namespace EnglishHub.Api.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync (
        HttpContext httpContext,
        IAuthenticationTokenStorage authenticationTokenStorage,
        IAuthenticationService authenticationService,
        IIdentityProvider identityProvider)
    {
        identityProvider.Current = authenticationTokenStorage.TryExtract(httpContext, out string token)
            ? await authenticationService.Authenticate(token, httpContext.RequestAborted)
            : User.Guest;

        await _next(httpContext);
    }
}