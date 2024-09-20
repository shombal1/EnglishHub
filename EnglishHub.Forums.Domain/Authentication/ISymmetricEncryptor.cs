namespace EnglishHub.Forums.Domain.Authentication;

public interface ISymmetricEncryptor
{
    public Task<string> Encrypt(string plainText, byte[] key, CancellationToken cancellationToken);
}

public interface ISymmetricDecryptor
{
    public Task<string> Decrypt(string encryptText, byte[] key, CancellationToken cancellationToken);
}