using EnglishHub.Search.Domain.Models;
using MediatR;

namespace EnglishHub.Search.Domain.UseCase.Index;

public record IndexCommand(Guid EntityId, SearchEntityType EntityType, string? Title, string? Text) : IRequest;