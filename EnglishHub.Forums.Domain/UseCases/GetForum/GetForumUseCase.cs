using EnglishHub.Forums.Domain.monitoring;
using EnglishHub.Forums.Domain.Models;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.GetForum;

public class GetForumUseCase(IGetForumStorage getForumStorage) : IRequestHandler<GetForumQuery, IEnumerable<Forum>>
{
    public async Task<IEnumerable<Forum>> Handle(GetForumQuery query,CancellationToken cancellationToken)
    {
        return await getForumStorage.GetForums(cancellationToken);
    }
}