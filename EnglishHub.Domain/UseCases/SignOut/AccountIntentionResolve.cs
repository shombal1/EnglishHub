using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;

namespace EnglishHub.Domain.UseCases.SignOut;

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