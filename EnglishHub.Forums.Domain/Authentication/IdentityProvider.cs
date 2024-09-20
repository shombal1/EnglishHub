namespace EnglishHub.Forums.Domain.Authentication;

public class IdentityProvider : IIdentityProvider
{
    public IIdentity Current { get; set; }
}
