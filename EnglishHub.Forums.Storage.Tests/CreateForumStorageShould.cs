using EnglishHub.Storage;
using EnglishHub.Storage.Storage;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace EnglishHub.Forums.Storage.Tests;

public class CreateForumStorageShould : IClassFixture<StorageTestFixture>
{
    private readonly StorageTestFixture _fixture;
    private readonly CreateForumStorage _sut;
    private readonly EnglishHubDbContext _dbContext;

    public CreateForumStorageShould(StorageTestFixture fixture)
    {
        _fixture = fixture;
        _dbContext = fixture.GetDbContext();
        _sut = new CreateForumStorage(_dbContext,fixture.GetMemoryCache(),fixture.GetMapper());
    }

    [Fact]
    public async Task AddNewForumInStorage()
    {
        string title = "Title";
        var forum = await _sut.CreateForum(title, CancellationToken.None);

        forum.Id.Should().NotBeEmpty();
        forum.Title.Should().BeEquivalentTo(title);

        var forums = await _dbContext.Forums
            .AsNoTracking()
            .Where(f => f.Id == forum.Id)
            .ToArrayAsync();

        forums.Should().HaveCount(1).And.Contain(f => f.Title == forum.Title);
    }
}