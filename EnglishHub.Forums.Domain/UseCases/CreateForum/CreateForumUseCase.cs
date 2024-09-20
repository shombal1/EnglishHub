using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Authorization;
using EnglishHub.Forums.Domain.Models;
using FluentValidation;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.CreateForum;

public class CreateForumUseCase(
    ICreateForumStorage storage,
    IValidator<CreateForumCommand> validator,
    IIntentionManager intentionManager)
    : IRequestHandler<CreateForumCommand, Forum>
{
    public async Task<Forum> Handle(CreateForumCommand command,CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command,cancellationToken);

        intentionManager.ThrowIfForbidden(ForumIntention.Create);
        
        return await storage.CreateForum(command.Title,cancellationToken);
    }
}