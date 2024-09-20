using AutoMapper;
using EnglishHub.Forums.Domain.UseCases.SignIn;
using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class SignInStorage(EnglishHubDbContext dbContext, IMapper mapper) : ISignInStorage
{
    public async Task<RecognizeUser?> FindUser(string login, CancellationToken cancellationToken)
    {
        return mapper.Map<RecognizeUser>(await dbContext.Users
            .FirstAsync(f=>f.Login==login, cancellationToken: cancellationToken)
        );
    }

    public async Task<Guid> CreateSession(Guid userId, DateTimeOffset expirationMoment, CancellationToken cancellationToken)
    {
        Guid sessionId = Guid.NewGuid();

        await dbContext.Sessions.AddAsync(new SessionEntity()
        {
            Id = sessionId,
            UserId = userId,
            Expires = expirationMoment
        }, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return sessionId;
    }
}