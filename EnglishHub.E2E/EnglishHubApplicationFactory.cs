using System.Security.Cryptography;
using EnglishHub.Storage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Testcontainers.PostgreSql;

namespace EnglishHub.E2E;

public class EnglishHubApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder().Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseConfiguration(new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "ConnectionStrings:EnglishHubDbContext", _postgreSqlContainer.GetConnectionString() },
                { "Authentication:Base64Key", Convert.ToBase64String(RandomNumberGenerator.GetBytes(24)) }
            }!).Build());

        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _postgreSqlContainer.StartAsync();

        EnglishHubDbContext dbContext = new EnglishHubDbContext(new DbContextOptionsBuilder<EnglishHubDbContext>()
            .UseNpgsql(_postgreSqlContainer.GetConnectionString()).Options);

        await dbContext.Database.MigrateAsync();
    }

    public new async Task DisposeAsync()
    {
        await _postgreSqlContainer.DisposeAsync();
    }
}