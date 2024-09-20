namespace EnglishHub.Forums.Api.Authentication;

public class AuthenticationTokenStorage : IAuthenticationTokenStorage
{
    private const string HeaderKey = "EnglishHub-AuthenticationKey";

    public bool TryExtract(HttpContext httpContext, out string token)
    {
        

        if (httpContext.Request.Cookies.TryGetValue(HeaderKey, out var values)
            && !string.IsNullOrWhiteSpace(values))
        {
            token = values;

            return true;
        }

        token = "";
        return false;
    }

    public void Store(HttpContext httpContext, string token)
    {
        httpContext.Response.Cookies.Append(HeaderKey,token);
    }
}