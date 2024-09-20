namespace EnglishHub.Forums.Api.Authentication;

public interface IAuthenticationTokenStorage
{
    public bool TryExtract(HttpContext httpContext, out string token);
    public void Store(HttpContext httpContent, string token);
}