using System.Security.Cryptography;
using System.Text;

namespace Cervione.Api.Generators;

public static class StringGenerator
{
    private static readonly char[] Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();    
    
    public static string Generate(int length)
    {
        byte[] bytes = new byte[length];

        using (var random = RandomNumberGenerator.Create())
        {
            random.GetBytes(bytes);
        }

        var sb = new StringBuilder(length);
        foreach (var b in bytes)
        {
            sb.Append(Characters[b % Characters.Length]);
        }

        return sb.ToString();
    }
}