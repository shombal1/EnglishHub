using EnglishHub.Domain.Exceptions;

namespace EnglishHub.Domain.UseCases.GetForumUseCase;

public static class GetForumStorageExtension
{
    public static async Task<bool> ForumExists(this IGetForumStorage storage,Guid forumId)
    {
        return (await storage.GetForums()).Any(f=>f.Id==forumId);
    }

    public static async Task ThrowIfForumNotFound(this IGetForumStorage storage,Guid forumId)
    {
        bool forumExists = await storage.ForumExists(forumId);
        if (!forumExists)
        {
            throw new ForumNotFoundException(forumId);
        }
    }

}