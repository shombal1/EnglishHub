using EnglishHub.Domain.Authentication;
using EnglishHub.Domain.Authorization;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.GetForum;
using FluentValidation;
using MediatR;

namespace EnglishHub.Domain.UseCases.CreateTopic;

public class CreateTopicUseCase(
    IIdentityProvider identityProvider,
    IGetForumStorage getForumStorage,
    IIntentionManager intentionManager,
    IValidator<CreateTopicCommand> validator,
    IUnitOfWork unitOfWork)
    : IRequestHandler<CreateTopicCommand, Topic>
{
    public async Task<Topic> Handle(CreateTopicCommand command,CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command,cancellationToken);

        intentionManager.ThrowIfForbidden(TopicIntention.Create);

        await getForumStorage.ThrowIfForumNotFound(command.ForumId,cancellationToken);

        await using (var scope = await unitOfWork.StartScope(cancellationToken))
        {
            var createTopic = scope.GetStorage<ICreateTopicStorage>();
            var domainEvent = scope.GetStorage<IDomainEventStorage>();
            
            var topic = await createTopic.CreateTopic(
                command.ForumId, 
                identityProvider.Current.UserId, 
                command.Title,
                cancellationToken);
            await domainEvent.AddEvent(topic, cancellationToken);
            
            await scope.Commit(cancellationToken);
            
            return topic;
        }
    }
}