using Konscious.Security.Cryptography;
using System.Text;
using TTCompanion.API.Entities;

namespace TTCompanion.API.Utils
{
    public static class Argon2Hashing
    {
        public static byte[] CreateSalt(DateTime time)
        {
            return Encoding.ASCII.GetBytes(time.ToString());
        }

        public static string HashPassword(string password, DateTime time)
        {
            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            var salt = CreateSalt(time);
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 8; // four cores
            argon2.Iterations = 4;
            argon2.MemorySize = 1024 * 1024; // 1 GB
            var bytes = argon2.GetBytes(16);
            return Convert.ToBase64String(bytes);
        }
    }
}
