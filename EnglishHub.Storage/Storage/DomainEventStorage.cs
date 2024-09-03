using System.Text.Json;
using EnglishHub.Domain.UseCases;
using EnglishHub.Storage.Models;

namespace EnglishHub.Storage.Storage;

public class DomainEventStorage(EnglishHubDbContext dbContext,TimeProvider timeProvider) : IDomainEventStorage
{
    public async Task AddEvent<TDomainEntity>(TDomainEntity entity, CancellationToken cancellationToken)
    {
        await dbContext.DomainEvents.AddAsync(new DomainEventEntity()
        {
            Id = Guid.NewGuid(),
            CalledEntity = JsonSerializer.Serialize(entity),
            Created = timeProvider.GetUtcNow()
        },cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}