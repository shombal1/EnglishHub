using EnglishHub.Domain.Authentication;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.UseCases.SignIn;

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
            throw new ValidationException(new ValidationFailure[]
            {
                new ValidationFailure()
                {
                    PropertyName = nameof(command.Login),
                    ErrorCode = "Invalid",
                    AttemptedValue = command.Login
                }
            });
        }

        bool passwordMatches = _passwordManager.ComparePassword(command.Password, recognizeUser.Salt, recognizeUser.PasswordHash);

        if (!passwordMatches)
        {
            throw new ValidationException(new ValidationFailure[]
            {
                new ValidationFailure()
                {
                    PropertyName = nameof(command.Password),
                    ErrorCode = "Invalid",
                    AttemptedValue = command.Password
                }
            });
        }
        // TODO: Generate time expiration moment
        Guid sessionId = await _storage.CreateSession(recognizeUser.UserId, 
            DateTimeOffset.UtcNow + TimeSpan.FromHours(1), cancellationToken);
        string token = await _symmetricEncryptor.Encrypt(sessionId.ToString(), _configuration.Key, cancellationToken);

        return (new User(recognizeUser.UserId,sessionId), token);
    }
    
}