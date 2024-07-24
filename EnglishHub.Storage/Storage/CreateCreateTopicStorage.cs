using AutoMapper;
using AutoMapper.QueryableExtensions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.CreateTopic;
using EnglishHub.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Storage.Storage;

public class CreateCreateTopicStorage : ICreateTopicStorage
{
    private readonly EnglishHubDbContext _dbContext;
    private readonly IMapper _mapper;

    public CreateCreateTopicStorage(EnglishHubDbContext dbContext,IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Topic> CreateTopic(Guid forumId, Guid authorId, string title,CancellationToken cancellationToken)
    {
        var newTopic = new TopicEntity
        {
            Id = Guid.NewGuid(),
            Title = title,
            PublicationAt = DateTimeOffset.UtcNow,
            AuthorId = authorId,
            ForumId = forumId,
        };

        await _dbContext.Topics.AddAsync(newTopic,cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return await _dbContext.Topics
            .Where(t => t.Id == newTopic.Id)
            .ProjectTo<Topic>(_mapper.ConfigurationProvider)
            .FirstAsync(cancellationToken);
    }
}