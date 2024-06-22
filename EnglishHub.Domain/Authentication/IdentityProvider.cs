namespace EnglishHub.Domain.Authentication;

public class IdentityProvider : IIdentityProvider
{
    public IIdentity Current => new User(Guid.Parse("666C612E-7AC2-4A06-BDD0-3EF145E58EC0"));
}