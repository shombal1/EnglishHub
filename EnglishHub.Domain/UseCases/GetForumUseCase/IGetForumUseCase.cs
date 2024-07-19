using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public interface IGetForumUseCase
{
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken);
}