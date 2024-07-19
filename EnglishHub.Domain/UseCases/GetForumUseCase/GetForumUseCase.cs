using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public class GetForumUseCase : IGetForumUseCase
{
    private readonly IGetForumStorage _getForumStorage;

    public GetForumUseCase(IGetForumStorage getForumStorage)
    {
        _getForumStorage = getForumStorage;
    }
    
    public Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken)
    {
        return _getForumStorage.GetForums(cancellationToken);
    }
}