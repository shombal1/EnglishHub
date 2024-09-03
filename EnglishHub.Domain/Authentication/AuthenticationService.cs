using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.Authentication;

public class AuthenticationService(
    ISymmetricDecryptor symmetricDecryptor,
    IOptions<AuthenticationConfiguration> configuration,
    IAuthenticationServiceStorage storage,
    ILogger<AuthenticationService> logger)
    : IAuthenticationService
{
    private readonly AuthenticationConfiguration _configuration = configuration.Value;

    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        string sessionIdString;
        try
        {
            sessionIdString = await symmetricDecryptor.Decrypt(authToken, _configuration.Key, cancellationToken);
        }
        catch (CryptographicException cryptographicException)
        {
            logger.LogWarning(
                cryptographicException,
                "authentication token cannot be decrypt, maybe someone is trying to crack the token");
            return User.Guest;
        }

        if (!Guid.TryParse(sessionIdString, out Guid sessionId))
        {
            return User.Guest;
        }

        Session? session = await storage.FindSession(sessionId, cancellationToken);

        if (session is null)
        {
            return User.Guest;
        }

        if (session.Expires < DateTimeOffset.UtcNow)
        {
            return User.Guest;
        }

        return new User(session.UserId, sessionId);
    }
}