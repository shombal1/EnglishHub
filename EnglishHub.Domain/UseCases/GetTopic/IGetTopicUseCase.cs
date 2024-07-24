using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetTopic;

public interface IGetTopicUseCase
{
    public Task<IEnumerable<Topic>> Execute(GetTopicRequest request,CancellationToken cancellationToken);
}