using AutoMapper;
using EnglishHub.Domain.Authentication;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class AuthenticationServiceStorage : IAuthenticationServiceStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMapper _mapper;

    public AuthenticationServiceStorage(
        EnglishHubDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Session?> FindSession(Guid sessionId, CancellationToken cancellationToken)
    {
        return _mapper.Map<Session>(await _dbContext.Sessions.FindAsync(sessionId));
    }
}