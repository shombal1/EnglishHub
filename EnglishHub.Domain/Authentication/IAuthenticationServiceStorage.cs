namespace EnglishHub.Domain.Authentication;

public interface IAuthenticationServiceStorage
{
    public Task<Session?> FindSession(Guid sessionId, CancellationToken cancellationToken);
}