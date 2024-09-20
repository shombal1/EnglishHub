using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Api.Authentication;

namespace EnglishHub.Forums.Api.Middleware;

public class AuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync (
        HttpContext httpContext,
        IAuthenticationTokenStorage authenticationTokenStorage,
        IAuthenticationService authenticationService,
        IIdentityProvider identityProvider)
    {
        identityProvider.Current = authenticationTokenStorage.TryExtract(httpContext, out string token)
            ? await authenticationService.Authenticate(token, httpContext.RequestAborted)
            : User.Guest;

        await next(httpContext);
    }
}