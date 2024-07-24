namespace EnglishHub.Domain.Authentication;

public interface IIdentity
{
    Guid UserId { get; }
    Guid SessionId { get; }
}

public static class IdentityExtension
{
    public static bool IsAuthenticated(this IIdentity identity) => identity.UserId != Guid.Empty;
}