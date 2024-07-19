using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace EnglishHub.Domain.Authentication;

public interface IAuthenticationService
{
    Task<IIdentity> Authenticate(string authToken, CancellationToken cancellationToken);
}
