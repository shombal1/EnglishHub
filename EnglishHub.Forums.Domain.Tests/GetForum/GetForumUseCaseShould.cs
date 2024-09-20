using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Forums.Domain.Tests.GetForum;

public class GetForumUseCaseShould
{
    public readonly GetForumUseCase sut;
    private readonly ISetup<IGetForumStorage,Task<IEnumerable<Forum>>> _getForumStorageSetup;
    
    
    public GetForumUseCaseShould()
    {
        var getForumStorage = new Mock<IGetForumStorage>();
        _getForumStorageSetup = getForumStorage.Setup(f => f.GetForums(It.IsAny<CancellationToken>()));
        
        sut = new GetForumUseCase(getForumStorage.Object);
    }

    [Fact]
    public async Task ReturnForums()
    {
        var expectedResources = new Forum[] { new() };
        _getForumStorageSetup.ReturnsAsync(expectedResources);

        var actual = await sut.Handle(new GetForumQuery(),CancellationToken.None);

        actual.Should().BeSameAs(expectedResources);
    }
}