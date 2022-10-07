using System.Security.Cryptography;
using System.Text;

namespace Nemesis.Api.Users;

public static class PasswordHasher
{
    public static string Hash(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);

        using var md5 = MD5.Create();

        var hash = md5.ComputeHash(bytes);

        return Encoding.UTF8.GetString(hash);
    }
}