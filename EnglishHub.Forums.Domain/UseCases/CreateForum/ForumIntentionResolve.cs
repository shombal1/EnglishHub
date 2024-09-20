using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Authorization;

namespace EnglishHub.Forums.Domain.UseCases.CreateForum;

public class ForumIntentionResolve : IIntentionResolve<ForumIntention>
{
    public bool IsAllowed(IIdentity subject, ForumIntention intention)
    {
        switch (intention)
        {
            case ForumIntention.Create:
                return subject.IsAuthenticated();
            default:
                return false;
        }
    }
}