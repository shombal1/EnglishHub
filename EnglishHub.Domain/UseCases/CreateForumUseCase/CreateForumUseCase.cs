using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using FluentValidation;

namespace EnglishHub.Domain.UseCases.CreateForumUseCase;

public class CreateForumUseCase : ICreateForumUseCase
{
    private readonly ICreateForumStorage _storage;
    private readonly IValidator<CreateForumCommand> _validator;
    private readonly IIntentionManager _intentionManager;
    private readonly IIdentityProvider _identityProvider;

    public CreateForumUseCase(ICreateForumStorage storage,
        IValidator<CreateForumCommand> validator,
        IIntentionManager intentionManager,
        IIdentityProvider identityProvider)
    {
        _storage = storage;
        _validator = validator;
        _intentionManager = intentionManager;
        _identityProvider = identityProvider;
    }
    
    public async Task<Forum> Execute(CreateForumCommand command,CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command,cancellationToken);

        _intentionManager.ThrowIfForbidden(ForumIntention.Create);
        
        return await _storage.CreateForum(command.Title,cancellationToken);
    }
}