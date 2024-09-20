using EnglishHub.Forums.Domain.Models;

namespace EnglishHub.Forums.Domain.UseCases.CreateForum;

public interface ICreateForumStorage
{
    public Task<Forum> CreateForum(string title,CancellationToken cancellationToken);
}