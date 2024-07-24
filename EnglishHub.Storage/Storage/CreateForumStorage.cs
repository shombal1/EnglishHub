using AutoMapper;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Storage.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishHub.Storage.Storage;

public class CreateForumStorage : ICreateForumStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public CreateForumStorage(EnglishHubDbContext dbContext, IMemoryCache memoryCache,IMapper mapper )
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }
    
    public async Task<Forum> CreateForum(string title,CancellationToken cancellationToken)
    {
        ForumEntity newForum = new ForumEntity()
        {
            Id = Guid.NewGuid(),
            Title = title
        };

        await _dbContext.Forums.AddAsync(newForum,cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
        
        _memoryCache.Remove(nameof(GetForumStorage.GetForums));

        return _mapper.Map<Forum>(await _dbContext.Forums.FindAsync(newForum.Id));
    }
}