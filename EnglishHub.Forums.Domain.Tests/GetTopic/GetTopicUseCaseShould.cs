using EnglishHub.Forums.Domain.Exceptions;
using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using EnglishHub.Forums.Domain.UseCases.GetTopic;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Moq.Language.Flow;

namespace EnglishHub.Forums.Domain.Tests.GetTopic;

public class GetTopicUseCaseShould
{
    private readonly GetTopicUseCase _sut;

    private readonly ISetup<IGetForumStorage, Task<IEnumerable<Forum>>> _getForumStorageSetup;
    private readonly ISetup<IGetTopicStorage, Task<IEnumerable<Topic>>> _getTopicStorageSetup;

    public GetTopicUseCaseShould()
    {
        var getTopicStorage = new Mock<IGetTopicStorage>();
        var getForumStorage = new Mock<IGetForumStorage>();
        var requestValidator = new Mock<IValidator<GetTopicQuery>>();

        _getForumStorageSetup = getForumStorage.Setup(f => 
            f.GetForums(It.IsAny<CancellationToken>()));
        _getTopicStorageSetup = getTopicStorage.Setup(t => 
            t.GetTopics(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<int>(),It.IsAny<CancellationToken>()));

        requestValidator.Setup(v => v.Validate(It.IsAny<GetTopicQuery>())).Returns(new ValidationResult());

        _sut = new GetTopicUseCase(getTopicStorage.Object, getForumStorage.Object, requestValidator.Object);
    }

    [Fact]
    public async Task ThrowForumNotFoundException_WhenNoFoundForum()
    {
        var idNonExistentForum = Guid.Parse("5E1DCF96-E8F3-41C9-BD59-6479140933B3");

        _getForumStorageSetup.ReturnsAsync(new[]
        {
            new Forum() { Id = Guid.Parse("9B958413-CC69-4498-837F-D6032A6B360E") }
        });

        await _sut.Invoking(s => s.Handle(new GetTopicQuery(idNonExistentForum, 0, 1),CancellationToken.None)).Should()
            .ThrowAsync<ForumNotFoundException>();
    }

    [Fact]
    public async Task ReturnTopics_WhenForumExists()
    {
        var forumId = Guid.Parse("6979D9B1-9941-455C-A3A0-8802C1A71255");

        _getForumStorageSetup.ReturnsAsync(new[] { new Forum() { Id = forumId, Title = "forum" } });

        var expectedResources = new Topic[] { new()};

        _getTopicStorageSetup.ReturnsAsync(expectedResources);

        var actualResources = await _sut.Handle(new GetTopicQuery(forumId, 6, 9),CancellationToken.None);

        actualResources.Should().BeEquivalentTo(expectedResources);
    }
}