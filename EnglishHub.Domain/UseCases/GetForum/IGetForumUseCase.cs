using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForum;

public interface IGetForumUseCase
{
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
}