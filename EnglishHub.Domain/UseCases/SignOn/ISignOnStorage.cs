namespace EnglishHub.Domain.UseCases.SignOn;

public interface ISignOnStorage
{
    public Task<Guid> CreateUser(string login, byte[] salt, byte[] passwordHash, CancellationToken cancellationToken);
}