namespace EnglishHub.Forums.Domain.UseCases.SignIn;

public interface ISignInStorage
{
    public Task<RecognizeUser?> FindUser(string login, CancellationToken cancellationToken);

    public Task<Guid> CreateSession(Guid userId,DateTimeOffset expirationMoment,CancellationToken cancellationToken);
}