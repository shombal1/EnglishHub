using AutoMapper;
using EnglishHub.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class AuthenticationServiceStorage(EnglishHubDbContext dbContext, IMapper mapper) : IAuthenticationServiceStorage
{
    public async Task<Session?> FindSession(Guid sessionId, CancellationToken cancellationToken)
    {
        return mapper.Map<Session>(await dbContext.Sessions.FindAsync(sessionId));
    }
}