namespace EnglishHub.Authentication;

public interface IAuthenticationTokenStorage
{
    public bool TryExtract(HttpContext httpContent, out string token);
    public void Store(HttpContext httpContent, string token);
}