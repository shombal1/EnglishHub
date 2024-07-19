using EnglishHub.Authentication;
using EnglishHub.Domain.Authentication;

namespace EnglishHub.Middleware;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        IAuthenticationTokenStorage authenticationTokenStorage,
        AuthenticationService authenticationService,
        IIdentityProvider identityProvider,
        CancellationToken cancellationToken)
    {
        identityProvider.Current = authenticationTokenStorage.TryExtract(httpContext, out string token)
            ? await authenticationService.Authenticate(token, cancellationToken)
            : User.Guest;

        await _next(httpContext);
    }
}