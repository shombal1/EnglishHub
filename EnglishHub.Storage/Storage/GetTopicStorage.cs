using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetTopicUseCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace EnglishHub.Storage.Storage;

public class GetTopicStorage : IGetTopicStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetTopicStorage(EnglishHubDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Topic>> GetTopics(Guid forumId, int skip, int take,CancellationToken cancellationToken)
    {
        return await _dbContext.Topics
            .AsNoTracking()
            .Where(a => a.ForumId == forumId)
            .Skip(skip).Take(take)
            .ProjectTo<Topic>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}