using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateForumUseCase;

public interface ICreateForumUseCase
{
    public Task<Forum> Execute(CreateForumCommand command,CancellationToken cancellationToken);
}