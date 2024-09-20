namespace EnglishHub.Forums.Domain.UseCases.SignOut;

public interface ISignOutStorage
{
    public Task RemoveSession(Guid sessionId, CancellationToken cancellationToken);
}