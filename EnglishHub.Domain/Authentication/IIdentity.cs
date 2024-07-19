namespace EnglishHub.Domain.Authentication;

public interface IIdentity
{
    Guid UserId { get; }
}

public class User : IIdentity
{
    public User(Guid userId)
    {
        UserId = userId;
    }

    public Guid UserId { get; }

    public static User Guest => new User(Guid.Empty);
}

public static class IdentityExtension
{
    public static bool IsAuthenticated(this IIdentity identity) => identity.UserId != Guid.Empty;
}