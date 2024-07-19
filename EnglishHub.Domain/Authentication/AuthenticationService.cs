using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.Authentication;


public class AuthenticationService : IAuthenticationService
{
    private readonly ISymmetricDecryptor _symmetricDecryptor;
    private readonly AuthenticationConfiguration _configuration;
    
    public AuthenticationService(ISymmetricDecryptor symmetricDecryptor,IOptions<AuthenticationConfiguration> configuration)
    {
        _symmetricDecryptor = symmetricDecryptor;
        _configuration = configuration.Value;
    }
    
    public async Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken)
    {
        string userIdString = await _symmetricDecryptor.Decrypt(authToken, _configuration.Key, cancellationToken);
        //TODO: Verify user
        return new User(Guid.Parse(userIdString));
    }
}