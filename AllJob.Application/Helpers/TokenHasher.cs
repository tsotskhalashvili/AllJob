using System.Security.Cryptography;
using System.Text;

namespace AllJob.Application.Helpers;

public static class TokenHasher
{
    public static string Hash(string token, string secret)
    {
        using var hmac = new HMACSHA256(
            Encoding.UTF8.GetBytes(secret));

        return Convert.ToHexString(
            hmac.ComputeHash(
                Encoding.UTF8.GetBytes(token)));
    }
}
