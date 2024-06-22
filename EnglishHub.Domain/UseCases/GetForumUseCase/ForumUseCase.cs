using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public class ForumUseCase : IForumUseCase
{
    private readonly IGetForumStorage _getForumStorage;

    public ForumUseCase(IGetForumStorage getForumStorage)
    {
        _getForumStorage = getForumStorage;
    }
    
    public Task<IEnumerable<Forum>> GetForums()
    {
        return _getForumStorage.GetForums();
    }
}