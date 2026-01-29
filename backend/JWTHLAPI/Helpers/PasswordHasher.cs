using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JWTHLAPI.Helpers
{
    public static class PasswordHasher
    {
        public static void CreatePasswordHash(
            string password,
            out string passwordHash,
            out string passwordSalt)
        {
            // .NET 5 compatible salt generation
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }

            passwordSalt = Convert.ToBase64String(saltBytes);

            byte[] hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            passwordHash = Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(
            string password,
            string storedHash,
            string storedSalt)
        {
            byte[] saltBytes = Convert.FromBase64String(storedSalt);

            byte[] hashBytes = KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 32);

            return Convert.ToBase64String(hashBytes) == storedHash;
        }
    }
}