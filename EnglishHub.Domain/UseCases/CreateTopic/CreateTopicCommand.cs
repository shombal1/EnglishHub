namespace EnglishHub.Domain.UseCases.CreateTopic;

public record CreateTopicCommand(Guid ForumId,string Title);