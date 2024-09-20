namespace EnglishHub.Forums.Domain.Exceptions;

public class ForumNotFoundException(Guid forumId) : DomainException(ErrorCode.Gone,$"Forum with the id {forumId} does not exists");