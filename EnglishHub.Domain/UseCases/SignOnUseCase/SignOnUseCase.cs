using EnglishHub.Domain.Authentication;
using FluentValidation;

namespace EnglishHub.Domain.UseCases.SignOnUseCase;

public class SignOnUseCase : ISignOnUseCase
{
    private readonly IValidator<SignOnCommand> _validator;
    private readonly ISignOnStorage _storage;
    private readonly IPasswordManager _passwordManager;

    public SignOnUseCase(
        IValidator<SignOnCommand> validator,
        ISignOnStorage storage,
        IPasswordManager passwordManager)
    {
        _validator = validator;
        _storage = storage;
        _passwordManager = passwordManager;
    }
    
    public async Task<IIdentity> Execute(SignOnCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);

        var (salt,passwordHash) =  _passwordManager.GeneratePassword(command.Password);
        
        Guid userId = await _storage.CreateUser(command.Login, salt, passwordHash, cancellationToken);

        return new User(userId);
    }
}