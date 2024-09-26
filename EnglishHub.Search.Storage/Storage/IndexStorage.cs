using EnglishHub.Search.Domain;
using EnglishHub.Search.Domain.UseCase.Index;
using EnglishHub.Search.Storage.Models;
using OpenSearch.Client;

namespace EnglishHub.Search.Storage.Storage;

public class IndexStorage(IOpenSearchClient openSearchClient) : IIndexStorage
{
    public async Task Index(Guid entityId, SearchEntityType entityType, string? title, string? text,
        CancellationToken cancellationToken)
    {
        // TODO observe idempotence when adding an entity
        await openSearchClient.IndexAsync(new SearchEntity
        {
            EntityId = entityId,
            EntityType = (int)entityType,
            Title = title,
            Text = text
        },selector=>selector,cancellationToken);
    }
}