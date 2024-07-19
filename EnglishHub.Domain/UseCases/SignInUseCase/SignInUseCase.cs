using EnglishHub.Domain.Authentication;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.UseCases.SignInUseCase;

public class SignInUseCase: ISignInUseCase
{
    private readonly IValidator<SignInCommand> _validator;
    private readonly ISignInStorage _storage;
    private readonly IPasswordManager _passwordManager;
    private readonly ISymmetricEncryptor _symmetricEncryptor;
    private readonly AuthenticationConfiguration _configuration;
    
    public SignInUseCase(
        IValidator<SignInCommand> validator,
        ISignInStorage storage,
        IPasswordManager passwordManager,
        ISymmetricEncryptor symmetricEncryptor,
        IOptions<AuthenticationConfiguration> options)
    {
        _validator = validator;
        _storage = storage;
        _passwordManager = passwordManager;
        _symmetricEncryptor = symmetricEncryptor;
        _configuration = options.Value;
    }
    
    public async Task<(IIdentity identity,string token)> Execute(SignInCommand command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command, cancellationToken);
        
        RecognizeUser? recognizeUser = await _storage.FindUser(command.Login, cancellationToken);

        if (recognizeUser is null)
        {
            throw new Exception("User not found");
        }

        bool passwordMatches = _passwordManager.ComparePassword(command.Password, recognizeUser.Salt, recognizeUser.PasswordHash);

        if (!passwordMatches)
        {
            throw new Exception();
        }

        string token = await _symmetricEncryptor.Encrypt(recognizeUser.UserId.ToString(), _configuration.Key, cancellationToken);

        return (new User(recognizeUser.UserId), token);
    }
    
}