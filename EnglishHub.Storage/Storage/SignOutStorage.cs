using EnglishHub.Domain.UseCases.SignOut;

namespace EnglishHub.Storage.Storage;

public class SignOutStorage : ISignOutStorage
{
    public Task RemoveSession(Guid sessionId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}