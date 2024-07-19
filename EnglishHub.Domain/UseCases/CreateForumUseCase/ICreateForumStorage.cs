using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateForumUseCase;

public interface ICreateForumStorage
{
    public Task<Forum> CreateForum(string title,CancellationToken cancellationToken);
}