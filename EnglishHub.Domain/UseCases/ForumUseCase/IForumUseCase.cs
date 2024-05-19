using EnglishHub.Domain.Module;

namespace EnglishHub.Domain.UseCases.ForumUseCase;

public interface IForumUseCase
{
    public Task<IEnumerable<Forum>> Get();
}