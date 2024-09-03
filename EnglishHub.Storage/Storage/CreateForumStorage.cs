using AutoMapper;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateForum;
using EnglishHub.Storage.Models;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishHub.Storage.Storage;

public class CreateForumStorage(EnglishHubDbContext dbContext, IMemoryCache memoryCache, IMapper mapper)
    : ICreateForumStorage
{
    public async Task<Forum> CreateForum(string title,CancellationToken cancellationToken)
    {
        ForumEntity newForum = new ForumEntity()
        {
            Id = Guid.NewGuid(),
            Title = title
        };

        await dbContext.Forums.AddAsync(newForum,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
        
        memoryCache.Remove(nameof(GetForumStorage.GetForums));

        return mapper.Map<Forum>(await dbContext.Forums.FindAsync(newForum.Id));
    }
}