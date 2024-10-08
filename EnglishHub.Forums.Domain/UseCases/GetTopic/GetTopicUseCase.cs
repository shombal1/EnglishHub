using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.GetForum;
using FluentValidation;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.GetTopic;

public class GetTopicUseCase(
    IGetTopicStorage storage,
    IGetForumStorage getForumStorage,
    IValidator<GetTopicQuery> validator)
    : IRequestHandler<GetTopicQuery, IEnumerable<Topic>>
{
    public async Task<IEnumerable<Topic>> Handle(GetTopicQuery query,CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(query,cancellationToken);

        await getForumStorage.ThrowIfForumNotFound(query.ForumId,cancellationToken);

        return await storage.GetTopics(query.ForumId, query.Skip, query.Take,cancellationToken);
    }
}