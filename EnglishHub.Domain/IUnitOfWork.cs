using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Domain;

public interface IUnitOfWork
{
    public Task<IUnitOfWorkScope> StartScope(CancellationToken cancellationToken);
}

public interface IUnitOfWorkScope : IAsyncDisposable
{
    public TStorage GetStorage<TStorage>() where TStorage : IStorage;
    public Task Commit(CancellationToken cancellationToken);
}