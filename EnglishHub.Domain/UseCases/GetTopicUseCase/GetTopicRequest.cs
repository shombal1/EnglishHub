namespace EnglishHub.Domain.UseCases.GetTopicUseCase;

public record class GetTopicRequest(Guid ForumId,int Skip,int Take);