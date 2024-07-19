using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.UseCases.SignInUseCase;

public interface ISignInStorage
{
    public Task<RecognizeUser?> FindUser(string login, CancellationToken cancellationToken);
}