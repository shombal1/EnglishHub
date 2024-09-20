using EnglishHub.Forums.Domain.Authentication;
using FluentValidation;
using MediatR;

namespace EnglishHub.Forums.Domain.UseCases.SignOn;

public class SignOnUseCase(
    IValidator<SignOnCommand> validator,
    ISignOnStorage storage,
    IPasswordManager passwordManager)
    : IRequestHandler<SignOnCommand, IIdentity>
{
    public async Task<IIdentity> Handle(SignOnCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);

        var (salt,passwordHash) =  passwordManager.GeneratePassword(command.Password);
        
        Guid userId = await storage.CreateUser(command.Login, salt, passwordHash, cancellationToken);

        return new User(userId,Guid.Empty);
    }
}