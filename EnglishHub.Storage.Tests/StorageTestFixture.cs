using System.Reflection;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Testcontainers.PostgreSql;

namespace EnglishHub.Storage.Tests;

public class StorageTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

    public EnglishHubDbContext GetDbContext() => new EnglishHubDbContext(
        new DbContextOptionsBuilder<EnglishHubDbContext>()
            .UseNpgsql(_postgreSqlContainer.GetConnectionString()).Options);

    public IMemoryCache GetMemoryCache() => new MemoryCache(new MemoryCacheOptions());

    public IMapper GetMapper() => new Mapper(new MapperConfiguration(m => 
            m.AddMaps(Assembly.GetAssembly(typeof(EnglishHubDbContext)))));

    public virtual async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        EnglishHubDbContext dbContext = new EnglishHubDbContext(new DbContextOptionsBuilder<EnglishHubDbContext>()
            .UseNpgsql(_postgreSqlContainer.GetConnectionString()).Options);

        await dbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}