using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Site.Domain._shared
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            // تولید Salt
            var salt = GenerateSalt();
            var combined = salt + password;

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
            var hash = Convert.ToBase64String(hashBytes);

            return $"{salt}:{hash}";
        }

        public static bool Verify(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = parts[0];
            var hash = parts[1];

            using var sha256 = SHA256.Create();
            var combined = salt + password;
            var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
            var computedHashStr = Convert.ToBase64String(computedHash);

            return hash == computedHashStr;
        }

        private static string GenerateSalt()
        {
            var randomBytes = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}