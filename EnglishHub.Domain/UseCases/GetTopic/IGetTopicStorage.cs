using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetTopic;

public interface IGetTopicStorage
{
    public Task<IEnumerable<Topic>> GetTopics(Guid forumId, int skip, int take,CancellationToken cancellationToken);
}