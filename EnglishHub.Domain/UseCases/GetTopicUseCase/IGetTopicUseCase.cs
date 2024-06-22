using EnglishHub.Domain.Models;

namespace EnglishHub.Domain.UseCases.GetTopicUseCase;

public interface IGetTopicUseCase
{
    public Task<IEnumerable<Topic>> Execute(GetTopicRequest request);
}