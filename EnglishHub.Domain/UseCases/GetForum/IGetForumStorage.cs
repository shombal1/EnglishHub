using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForum;

public interface IGetForumStorage
{
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
}