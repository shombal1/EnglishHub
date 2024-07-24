using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateTopic;

public interface ICreateTopicUseCase
{
    Task<Topic> Execute(CreateTopicCommand command,CancellationToken cancellationToken);
}