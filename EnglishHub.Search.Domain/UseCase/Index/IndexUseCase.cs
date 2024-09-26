using EnglishHub.Search.Domain.Models;
using MediatR;

namespace EnglishHub.Search.Domain.UseCase.Index;

public class IndexUseCase(IIndexStorage storage) : IRequestHandler<IndexCommand>
{
    public Task Handle(IndexCommand request, CancellationToken cancellationToken)
    {
        var (entityId, entityType, title, text) = request;
        return storage.Index(entityId, entityType, title, text, cancellationToken);
    }
}