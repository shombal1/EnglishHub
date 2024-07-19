

using EnglishHub.Domain.Authentication;

namespace EnglishHub.Domain.UseCases.SignOnUseCase;

public interface ISignOnUseCase
{
    public Task<IIdentity> Execute(SignOnCommand command, CancellationToken cancellationToken);
}