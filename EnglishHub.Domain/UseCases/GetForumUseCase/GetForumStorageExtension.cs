using EnglishHub.Domain.Exceptions;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public static class GetForumStorageExtension
{
    public static async Task<bool> ForumExists(this IGetForumStorage storage,Guid forumId,CancellationToken cancellationToken)
    {
        return (await storage.GetForums(cancellationToken)).Any(f=>f.Id==forumId);
    }

    public static async Task ThrowIfForumNotFound(this IGetForumStorage storage,Guid forumId,CancellationToken cancellationToken)
    {
        bool forumExists = await storage.ForumExists(forumId,cancellationToken);
        if (!forumExists)
        {
            throw new ForumNotFoundException(forumId);
        }
    }

}