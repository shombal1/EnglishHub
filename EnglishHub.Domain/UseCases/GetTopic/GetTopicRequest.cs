namespace EnglishHub.Domain.UseCases.GetTopic;

public record class GetTopicRequest(Guid ForumId,int Skip,int Take);