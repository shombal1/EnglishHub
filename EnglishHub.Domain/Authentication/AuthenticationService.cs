using System.Security.Cryptography;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.Authentication;


public class AuthenticationService : IAuthenticationService
{
    private readonly ISymmetricDecryptor _symmetricDecryptor;
    private readonly IAuthenticationServiceStorage _storage;
    private readonly ILogger<AuthenticationService> _logger;
    private readonly AuthenticationConfiguration _configuration;
    
    public AuthenticationService(
        ISymmetricDecryptor symmetricDecryptor,
        IOptions<AuthenticationConfiguration> configuration,
        IAuthenticationServiceStorage storage,
        ILogger<AuthenticationService> logger)
    {
        _symmetricDecryptor = symmetricDecryptor;
        _storage = storage;
        _logger = logger;
        _configuration = configuration.Value;
    }
    
    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        string sessionIdString;
        try
        {
            sessionIdString = await _symmetricDecryptor.Decrypt(authToken, _configuration.Key, cancellationToken);
        }
        catch (CryptographicException cryptographicException)
        {
            _logger.LogWarning(
                cryptographicException,
                "authentication token cannot be decrypt, maybe someone is trying to crack the token");
            return User.Guest;
        }

        if (!Guid.TryParse(sessionIdString, out Guid sessionId))
        {
            return User.Guest;
        }

        Session? session = await _storage.FindSession(sessionId, cancellationToken);

        if (session is null)
        {
            return User.Guest;
        }

        if (session.Expires < DateTimeOffset.UtcNow)
        {
            return User.Guest;
        }
        
        return new User(session.UserId,sessionId);
    }
}