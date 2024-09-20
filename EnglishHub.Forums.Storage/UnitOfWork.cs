using EnglishHub.Forums.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace EnglishHub.Storage;

public class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
{
    public async Task<IUnitOfWorkScope> StartScope(CancellationToken cancellationToken)
    {
        var scope = serviceProvider.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<EnglishHubDbContext>();
        var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);

        return new UnitOfWorkScope(transaction,scope);
    }
}

public class UnitOfWorkScope(IDbContextTransaction transaction,IServiceScope serviceScope) : IUnitOfWorkScope
{
    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
        if (serviceScope is AsyncServiceScope asyncServiceScope)
        {
            await asyncServiceScope.DisposeAsync();
        }
        else
        {
            serviceScope.Dispose();
        }
    }

    public TStorage GetStorage<TStorage>() where TStorage : IStorage
    {
        return serviceScope.ServiceProvider.GetRequiredService<TStorage>();
    }

    public async Task Commit(CancellationToken cancellationToken)
    {
        await transaction.CommitAsync(cancellationToken);
    }
}