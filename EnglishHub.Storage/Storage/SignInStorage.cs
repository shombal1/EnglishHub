using AutoMapper;
using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.UseCases.SignIn;
using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class SignInStorage : ISignInStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMapper _mapper;

    public SignInStorage(EnglishHubDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<RecognizeUser?> FindUser(string login, CancellationToken cancellationToken)
    {
        return _mapper.Map<RecognizeUser>(await _dbContext.Users
            .FirstAsync(f=>f.Login==login, cancellationToken: cancellationToken)
        );
    }

    public async Task<Guid> CreateSession(Guid userId, DateTimeOffset expirationMoment, CancellationToken cancellationToken)
    {
        Guid sessionId = Guid.NewGuid();

        await _dbContext.Sessions.AddAsync(new SessionEntity()
        {
            Id = sessionId,
            UserId = userId,
            Expires = expirationMoment
        }, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return sessionId;
    }
}