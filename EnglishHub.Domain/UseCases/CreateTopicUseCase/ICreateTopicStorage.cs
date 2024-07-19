using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateTopicUseCase;

public interface ICreateTopicStorage
{
    public Task<Topic> CreateTopic(Guid forumId, Guid authorId, string title,CancellationToken cancellationToken);
}