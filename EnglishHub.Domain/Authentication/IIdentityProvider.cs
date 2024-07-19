namespace EnglishHub.Domain.Authentication;

public interface IIdentityProvider
{
    IIdentity Current { get; set; }
}
