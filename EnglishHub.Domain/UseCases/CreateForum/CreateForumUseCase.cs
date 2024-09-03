using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using FluentValidation;
using MediatR;

namespace EnglishHub.Domain.UseCases.CreateForum;

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