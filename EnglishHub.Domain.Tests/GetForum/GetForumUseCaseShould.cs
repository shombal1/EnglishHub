using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForum;
using FluentAssertions;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Domain.Tests.GetForum;

public class GetForumUseCaseShould
{
    public readonly IGetForumUseCase sut;
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

        var actual = await sut.GetForums(CancellationToken.None);

        actual.Should().BeSameAs(expectedResources);
    }
}