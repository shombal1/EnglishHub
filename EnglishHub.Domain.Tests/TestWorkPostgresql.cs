using System.Diagnostics;
using EnglishHub.Storage;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EnglishHub.Domain.Tests;

public class TestWorkPostgresql
{
    private readonly EnglishHubDbContext _dbContext;

    public TestWorkPostgresql()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.GetFullPath(@"..\..\..\..\EnglishHub.Api\appsettings.json"))
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EnglishHubDbContext>();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("EnglishHubDbContext"));

        _dbContext = new EnglishHubDbContext(optionsBuilder.Options);
    }

    [Fact]
    public void CheckConnection()
    {
        bool connect = _dbContext.Database.CanConnect();

        connect.Should().BeTrue();
    }
}