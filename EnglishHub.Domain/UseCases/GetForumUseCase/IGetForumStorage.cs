using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public interface IGetForumStorage
{
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
}