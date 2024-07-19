using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.CreateTopicUseCase;

public interface ICreateTopicUseCase
{
    Task<Topic> Execute(CreateTopicCommand command,CancellationToken cancellationToken);
}