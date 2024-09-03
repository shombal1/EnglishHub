using EnglishHub.Domain.Authentication;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.UseCases.SignIn;

public class SignInUseCase(
    IValidator<SignInCommand> validator,
    ISignInStorage storage,
    IPasswordManager passwordManager,
    ISymmetricEncryptor symmetricEncryptor,
    IOptions<AuthenticationConfiguration> options,
    TimeProvider timeProvider)
    : IRequestHandler<SignInCommand, (IIdentity identity, string token)>
{
    private readonly AuthenticationConfiguration _configuration = options.Value;

    public async Task<(IIdentity identity,string token)> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(command, cancellationToken);
        
        RecognizeUser? recognizeUser = await storage.FindUser(command.Login, cancellationToken);

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

        bool passwordMatches = passwordManager.ComparePassword(command.Password, recognizeUser.Salt, recognizeUser.PasswordHash);

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
        Guid sessionId = await storage.CreateSession(recognizeUser.UserId, 
            timeProvider.GetUtcNow() + TimeSpan.FromHours(1), cancellationToken);
        string token = await symmetricEncryptor.Encrypt(sessionId.ToString(), _configuration.Key, cancellationToken);

        return (new User(recognizeUser.UserId,sessionId), token);
    }
    
}