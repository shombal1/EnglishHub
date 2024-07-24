using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateForum;

public interface ICreateForumUseCase
{
    public Task<Forum> Execute(CreateForumCommand command,CancellationToken cancellationToken);
}