using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;

namespace EnglishHub.Domain.UseCases.CreateForum;

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