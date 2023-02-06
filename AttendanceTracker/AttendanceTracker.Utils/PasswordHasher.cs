using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AttendanceTracker.Utils;


public static class PasswordHasher
{
    public static HashedPassword HashPassword(string pwd)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(16);
        var hashBytes = KeyDerivation.Pbkdf2(
                            password: pwd,
                            salt: saltBytes,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 32);

        var salt = Convert.ToBase64String(saltBytes);
        var hashedPassword = Convert.ToBase64String(hashBytes);

        return new HashedPassword(hashedPassword, salt);
    }


    public static bool ComparePasswords(string plain, string salt, string hashed)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var hashBytes = KeyDerivation.Pbkdf2(
                            password: plain,
                            salt: saltBytes,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 32);

        var hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword == hashed;
    }
}
