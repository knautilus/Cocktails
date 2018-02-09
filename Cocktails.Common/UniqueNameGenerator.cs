using System;
using System.Security.Cryptography;

namespace Cocktails.Common
{
    public class UniqueNameGenerator
    {
        public static string Generate(string prefix)
        {
            var milliseconds = GetMilliseconds(12);
            var random = GetRandomDigits(4);
            return string.Concat(prefix, milliseconds, random);
        }

        public static string GetMilliseconds(uint length)
        {
            var ms = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() % Math.Pow(10, length);
            return ms.ToString(new string('0', (int) length));
        }

        public static string GetRandomDigits(uint length)
        {
            var bytes = new byte[4];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            var random = BitConverter.ToUInt32(bytes, 0) % Math.Pow(10, length);
            return random.ToString(new string('0', (int) length));
        }
    }
}
