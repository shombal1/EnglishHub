namespace EnglishHub.Domain.UseCases.SignOnUseCase;

public interface ISignOnStorage
{
    public Task<Guid> CreateUser(string login, byte[] salt, byte[] passwordHash, CancellationToken cancellationToken);
}