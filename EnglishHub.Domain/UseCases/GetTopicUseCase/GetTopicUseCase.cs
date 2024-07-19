using EnglishHub.Domain.Exceptions;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForumUseCase;
using FluentValidation;

namespace EnglishHub.Domain.UseCases.GetTopicUseCase;

public class GetTopicUseCase : IGetTopicUseCase
{
    private readonly IGetTopicStorage _storage;
    private readonly IGetForumStorage _getForumStorage;
    private readonly IValidator<GetTopicRequest> _validator;

    public GetTopicUseCase(IGetTopicStorage storage,IGetForumStorage getForumStorage,IValidator<GetTopicRequest> validator)
    {
        _storage = storage;
        _getForumStorage = getForumStorage;
        _validator = validator;
    }

    public async Task<IEnumerable<Topic>> Execute(GetTopicRequest request,CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request,cancellationToken);

        await _getForumStorage.ThrowIfForumNotFound(request.ForumId,cancellationToken);

        return await _storage.GetTopics(request.ForumId, request.Skip, request.Take,cancellationToken);
    }
}