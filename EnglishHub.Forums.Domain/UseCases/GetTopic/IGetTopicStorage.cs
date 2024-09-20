using EnglishHub.Forums.Domain.Models;

namespace EnglishHub.Forums.Domain.UseCases.GetTopic;

public interface IGetTopicStorage
{
    public Task<IEnumerable<Topic>> GetTopics(Guid forumId, int skip, int take,CancellationToken cancellationToken);
}