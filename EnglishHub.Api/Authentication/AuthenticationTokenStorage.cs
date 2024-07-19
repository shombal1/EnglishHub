namespace EnglishHub.Authentication;

public class AuthenticationTokenStorage : IAuthenticationTokenStorage
{
    private const string HeaderKey = "EnglishHub-AuthenticationKey";

    public bool TryExtract(HttpContext httpContent, out string token)
    {
        if (httpContent.Request.Headers.TryGetValue(HeaderKey, out var values)
            && !string.IsNullOrWhiteSpace(values.FirstOrDefault()))
        {
            token = values.First()!;

            return true;
        }

        token = "";
        return false;
    }

    public void Store(HttpContext httpContent, string token)
    {
        httpContent.Response.Headers[HeaderKey] = token;
    }
}