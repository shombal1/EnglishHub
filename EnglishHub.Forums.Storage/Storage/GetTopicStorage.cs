using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.GetTopic;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class GetTopicStorage(EnglishHubDbContext dbContext, IMapper mapper) : IGetTopicStorage
{
    public async Task<IEnumerable<Topic>> GetTopics(Guid forumId, int skip, int take,CancellationToken cancellationToken)
    {
        return await dbContext.Topics
            .AsNoTracking()
            .Where(a => a.ForumId == forumId)
            .Skip(skip).Take(take)
            .ProjectTo<Topic>(mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }
}