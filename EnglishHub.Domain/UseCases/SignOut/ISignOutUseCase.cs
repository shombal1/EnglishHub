namespace EnglishHub.Domain.UseCases.SignOut;

public interface ISignOutUseCase
{
    public Task Execute(CancellationToken cancellationToken);
}