using System.Security.Cryptography;
using System.Text;

namespace EnglishHub.Forums.Domain.Authentication;

public class TripleDesSymmetricEncryptorDecryptor: ISymmetricEncryptor,ISymmetricDecryptor
{
    private const int IvSize = 8;
    private readonly Lazy<TripleDES> _tripleDes = new Lazy<TripleDES>(TripleDES.Create);
    
    public async Task<string> Encrypt(string plainText, byte[] key, CancellationToken cancellationToken)
    {
        byte[] iv = RandomNumberGenerator.GetBytes(IvSize);

        using var encryptStream = new MemoryStream();
        await encryptStream.WriteAsync(iv, cancellationToken);
        var encryptor = _tripleDes.Value.CreateEncryptor(key, iv);
        await using (var stream = new CryptoStream(
                         encryptStream,
                         encryptor,
                         CryptoStreamMode.Write))
        {
            await stream.WriteAsync(Encoding.UTF8.GetBytes(plainText),cancellationToken);
        }

        return Convert.ToBase64String(encryptStream.ToArray());
    }

    public async Task<string> Decrypt(string encryptText, byte[] key, CancellationToken cancellationToken)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptText);

        byte[] iv = new byte[IvSize];
        Array.Copy(encryptedBytes,0,iv,0,IvSize);
        
        using var decryptStream = new MemoryStream();
        var decryptor = _tripleDes.Value.CreateDecryptor(key,iv);

        await using (var stream = new CryptoStream(
                   decryptStream,
                   decryptor,
                   CryptoStreamMode.Write))
        {
            await stream.WriteAsync(encryptedBytes.AsMemory(IvSize), cancellationToken);
        }

        return Encoding.UTF8.GetString(decryptStream.ToArray());
    }
}