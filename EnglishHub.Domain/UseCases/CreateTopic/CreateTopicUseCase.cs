using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForum;
using FluentValidation;

namespace EnglishHub.Domain.UseCases.CreateTopic;

public class CreateTopicUseCase : ICreateTopicUseCase
{
    private readonly IIntentionManager _intentionManager;
    private readonly IValidator<CreateTopicCommand> _validator;
    private readonly IIdentityProvider _identityProvider;
    private readonly ICreateTopicStorage _storage;
    private readonly IGetForumStorage _getForumStorage;

    public CreateTopicUseCase(IIdentityProvider identityProvider,
        ICreateTopicStorage storage, IGetForumStorage getForumStorage, IIntentionManager intentionManager,
        IValidator<CreateTopicCommand> validator)
    {
        _intentionManager = intentionManager;
        _validator = validator;
        _identityProvider = identityProvider;
        _storage = storage;
        _getForumStorage = getForumStorage;
    }

    public async Task<Topic> Execute(CreateTopicCommand command,CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command,cancellationToken);

        _intentionManager.ThrowIfForbidden(TopicIntention.Create);

        await _getForumStorage.ThrowIfForumNotFound(command.ForumId,cancellationToken);

        return await _storage.CreateTopic(command.ForumId, _identityProvider.Current.UserId, command.Title,cancellationToken);
    }
}