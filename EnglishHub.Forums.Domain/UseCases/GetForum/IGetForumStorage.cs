using EnglishHub.Forums.Domain.Models;

namespace EnglishHub.Forums.Domain.UseCases.GetForum;

public interface IGetForumStorage
{
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
}