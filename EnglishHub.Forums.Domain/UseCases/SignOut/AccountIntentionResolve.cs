using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Authorization;

namespace EnglishHub.Forums.Domain.UseCases.SignOut;

public class AccountIntentionResolve : IIntentionResolve<AccountIntention>
{
    public bool IsAllowed(IIdentity subject, AccountIntention intention)
    {
        return intention switch
        {
            AccountIntention.SignOut => subject.IsAuthenticated(),
            _ => false
        };
    }
}