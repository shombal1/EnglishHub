using EnglishHub.Forums.Domain.Models;

namespace EnglishHub.Forums.Domain.UseCases.CreateTopic;

public interface ICreateTopicStorage : IStorage
{
    public Task<Topic> CreateTopic(Guid forumId, Guid authorId, string title,CancellationToken cancellationToken);
}