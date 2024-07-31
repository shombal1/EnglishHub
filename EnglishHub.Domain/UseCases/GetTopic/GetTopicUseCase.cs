using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForum;
using FluentValidation;
using MediatR;

namespace EnglishHub.Domain.UseCases.GetTopic;

public class GetTopicUseCase : IRequestHandler<GetTopicQuery,IEnumerable<Topic>>
{
    private readonly IGetTopicStorage _storage;
    private readonly IGetForumStorage _getForumStorage;
    private readonly IValidator<GetTopicQuery> _validator;

    public GetTopicUseCase(IGetTopicStorage storage,IGetForumStorage getForumStorage,IValidator<GetTopicQuery> validator)
    {
        _storage = storage;
        _getForumStorage = getForumStorage;
        _validator = validator;
    }

    public async Task<IEnumerable<Topic>> Handle(GetTopicQuery query,CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(query,cancellationToken);

        await _getForumStorage.ThrowIfForumNotFound(query.ForumId,cancellationToken);

        return await _storage.GetTopics(query.ForumId, query.Skip, query.Take,cancellationToken);
    }
}