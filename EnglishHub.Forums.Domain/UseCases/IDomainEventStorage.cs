namespace EnglishHub.Forums.Domain.UseCases;

public interface IDomainEventStorage : IStorage
{
    public Task AddEvent<TDomainEntity>(TDomainEntity entity, CancellationToken cancellationToken);
}