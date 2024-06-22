namespace EnglishHub.Domain.UseCases.CreateTopicUseCase;

public record CreateTopicCommand(Guid ForumId,string Title);