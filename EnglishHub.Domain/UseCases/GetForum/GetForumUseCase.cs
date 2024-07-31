using EnglishHub.Domain.Models;
using EnglishHub.Domain.monitoring;
using MediatR;

namespace EnglishHub.Domain.UseCases.GetForum;

public class GetForumUseCase : IRequestHandler<GetForumQuery,IEnumerable<Forum>>
{
    private readonly IGetForumStorage _getForumStorage;

    public GetForumUseCase(
        IGetForumStorage getForumStorage)
    {
        _getForumStorage = getForumStorage;
    }
    
    public async Task<IEnumerable<Forum>> Handle(GetForumQuery query,CancellationToken cancellationToken)
    {
        return await _getForumStorage.GetForums(cancellationToken);
    }
}