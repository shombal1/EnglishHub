namespace EnglishHub.Domain.Exceptions;

public class ForumNotFoundException : DomainException
{
    public ForumNotFoundException(Guid forumId) : base(ErrorCode.Gone,$"Forum with the id {forumId} does not exists")
    {

    }
}