using System.Security.Cryptography;
using System.Text;

namespace EnglishHub.Forums.Domain.Authentication;

public class PasswordManager : IPasswordManager
{
    private readonly Lazy<SHA256> _sha256 = new Lazy<SHA256>(SHA256.Create);

    public bool ComparePassword(string password, byte[] salt, byte[] hash)
    {
        var newHash = ComputeSha(password, salt);
        return newHash.SequenceEqual(hash);
    }

    public (byte[] Salt, byte[] Hash) GeneratePassword(string password)
    {
        int saltLength = 100;

        byte[] salt = RandomNumberGenerator.GetBytes(saltLength);
        byte[] newHash = ComputeSha(password, salt);
        
        return (salt,newHash);
    }

    private byte[] ComputeSha(string plainText, byte[] salt)
    {
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        
        byte[] buffer = new byte[plainTextBytes.Length+salt.Length];
        Array.Copy(plainTextBytes,0,buffer,0,plainTextBytes.Length);
        Array.Copy(salt,0,buffer,plainTextBytes.Length,salt.Length);

        lock (_sha256)
        {
            return _sha256.Value.ComputeHash(buffer);   
        }
    }
}