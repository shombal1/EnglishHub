namespace EnglishHub.Domain.UseCases;

public interface IDomainEventStorage : IStorage
{
    public Task AddEvent<TDomainEntity>(TDomainEntity entity, CancellationToken cancellationToken);
}