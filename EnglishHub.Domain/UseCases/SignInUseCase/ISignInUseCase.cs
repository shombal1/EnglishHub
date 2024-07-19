using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.UseCases.SignInUseCase;

public interface ISignInUseCase
{
    public Task<(IIdentity identity,string token)> Execute(SignInCommand command, CancellationToken cancellationToken);
}