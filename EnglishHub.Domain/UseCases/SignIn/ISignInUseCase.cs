using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.UseCases.SignIn;

public interface ISignInUseCase
{
    public Task<(IIdentity identity,string token)> Execute(SignInCommand command, CancellationToken cancellationToken);
}