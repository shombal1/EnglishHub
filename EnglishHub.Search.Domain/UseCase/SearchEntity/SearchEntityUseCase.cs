using EnglishHub.Search.Domain.Models;
using FluentValidation;
using MediatR;

namespace EnglishHub.Search.Domain.UseCase.SearchEntity;

public class SearchEntityUseCase(ISearchEntityStorage storage, IValidator<SearchEntityQuery> validator)
    : IRequestHandler<SearchEntityQuery, (IEnumerable<SearchResult> responses, int totalCount)>
{
    public async Task<(IEnumerable<SearchResult> responses, int totalCount)> Handle(SearchEntityQuery request,
        CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request,cancellationToken);

        return await storage.Search(request.Query, cancellationToken);
    }
}