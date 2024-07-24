using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;

namespace EnglishHub.Domain.UseCases.CreateTopic;

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