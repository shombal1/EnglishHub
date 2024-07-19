using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetTopicUseCase;

public interface IGetTopicStorage
{
    public Task<IEnumerable<Topic>> GetTopics(Guid forumId, int skip, int take,CancellationToken cancellationToken);
}