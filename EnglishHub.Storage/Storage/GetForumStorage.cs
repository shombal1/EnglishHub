using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishHub.Storage.Storage;

public class GetForumStorage(EnglishHubDbContext dbContext, IMemoryCache memoryCache, IMapper mapper)
    : IGetForumStorage
{
    public async Task<IEnumerable<Forum>> GetForums(CancellationToken cancellationToken)
    {
        return (await memoryCache.GetOrCreateAsync(
            nameof(GetForums),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                
                return dbContext.Forums
                    .AsTracking()
                    .ProjectTo<Forum>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
            }))!;
    }
}