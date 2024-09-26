using EnglishHub.Search.Domain.Models;

namespace EnglishHub.Search.Domain.UseCase.SearchEntity;

public interface ISearchEntityStorage
{
    public Task<(IEnumerable<SearchResult> responses, int totalCount)> Search(string queue,
        CancellationToken cancellationToken);
}