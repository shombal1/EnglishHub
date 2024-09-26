namespace EnglishHub.Search.Domain.UseCase.Index;

public interface IIndexStorage
{
    public Task Index(Guid entityId, SearchEntityType entityType, string? title, string? text,CancellationToken cancellationToken);
}