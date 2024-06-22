using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public interface IForumUseCase
{
    public Task<IEnumerable<Forum>> GetForums();
}