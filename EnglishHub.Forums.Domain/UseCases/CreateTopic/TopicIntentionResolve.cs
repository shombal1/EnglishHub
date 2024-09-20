using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Authorization;

namespace EnglishHub.Forums.Domain.UseCases.CreateTopic;

public class TopicIntentionResolve : IIntentionResolve<TopicIntention>
{
    public bool IsAllowed(IIdentity subject, TopicIntention intention)
    {
        switch (intention)
        {
            case TopicIntention.Create:
                return subject.IsAuthenticated();
            default:
                return false;
        }
    }
}