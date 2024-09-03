using EnglishHub.Domain.Models;
using EnglishHub.Domain.monitoring;
using MediatR;

namespace EnglishHub.Domain.UseCases.GetForum;

public class GetForumUseCase(IGetForumStorage getForumStorage) : IRequestHandler<GetForumQuery, IEnumerable<Forum>>
{
    public async Task<IEnumerable<Forum>> Handle(GetForumQuery query,CancellationToken cancellationToken)
    {
        return await getForumStorage.GetForums(cancellationToken);
    }
}