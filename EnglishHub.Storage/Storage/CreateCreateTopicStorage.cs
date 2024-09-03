using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class CreateCreateTopicStorage(EnglishHubDbContext dbContext, IMapper mapper,TimeProvider timeProvider) : ICreateTopicStorage
{
    public async Task<Topic> CreateTopic(Guid forumId, Guid authorId, string title,CancellationToken cancellationToken)
    {
        var newTopic = new TopicEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            PublicationAt = timeProvider.GetUtcNow(),
            AuthorId = authorId,
            ForumId = forumId,
        };

        await dbContext.Topics.AddAsync(newTopic,cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return await dbContext.Topics
            .Where(t => t.Id == newTopic.Id)
            .ProjectTo<Topic>(mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}