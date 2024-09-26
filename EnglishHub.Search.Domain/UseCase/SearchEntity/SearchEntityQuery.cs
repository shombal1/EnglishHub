using EnglishHub.Search.Domain.Models;
using MediatR;

namespace EnglishHub.Search.Domain.UseCase.SearchEntity;

public record SearchEntityQuery(string Query) : IRequest<(IEnumerable<SearchResult> responses, int totalCount)>;