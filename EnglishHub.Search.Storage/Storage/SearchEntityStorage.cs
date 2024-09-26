using EnglishHub.Search.Domain;
using EnglishHub.Search.Domain.Models;
using EnglishHub.Search.Domain.UseCase.SearchEntity;
using OpenSearch.Client;
using SearchEntity = EnglishHub.Search.Storage.Models.SearchEntity;

namespace EnglishHub.Search.Storage.Storage;

public class SearchEntityStorage(IOpenSearchClient openSearchClient) : ISearchEntityStorage
{
    public async Task<(IEnumerable<SearchResult> responses, int totalCount)> Search(string queue,
        CancellationToken cancellationToken)
    {
        var searchResponse = await openSearchClient.SearchAsync<SearchEntity>(selector =>
            selector
                .Query(q => q
                    .Bool(b => b
                        .Should(s => s
                            .Match(m => m
                                .Field(f => f.Title).Query(queue)
                            ), s => s
                            .Match(m => m
                                .Field(f => f.Text).Query(queue).Fuzziness(Fuzziness.EditDistance(1))))))
                .Highlight(h=>h
                    .Fields(f=>f
                        .Field(s=>s.Title),f=>f
                        .Field(s=>s.Text)))
                .Sort(s=>s
                    .Field(new Field("_score"),SortOrder.Ascending)), cancellationToken);

        return (searchResponse.Hits.Select(hit => new SearchResult()
            {
                EntityId = hit.Source.EntityId,
                EntityType = (SearchEntityType)hit.Source.EntityType,
                Highlights = hit.Highlight.SelectMany(h=>h.Value).ToArray()
            }),(int)searchResponse.Total);
    }
}