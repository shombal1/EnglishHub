using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishHub.Storage.Storage;

public class GetForumStorage : IGetForumStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    private readonly IMapper _mapper;

    public GetForumStorage(EnglishHubDbContext dbContext, IMemoryCache memoryCache,IMapper mapper)
    {
        _dbContext = dbContext;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Forum>> GetForums()
    {
        return (await _memoryCache.GetOrCreateAsync(
            nameof(GetForums),
            entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                return _dbContext.Forums
                    .AsTracking()
                    .ProjectTo<Forum>(_mapper.ConfigurationProvider)
                    .ToArrayAsync();
            }))!;
    }
}