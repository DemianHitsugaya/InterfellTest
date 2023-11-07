using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Security
    {
        private const int SSize = 16;
        private const int KSize = 32;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char SDelimeter = ';';

        public static string Hash(string psw)
        {
            var salt = RandomNumberGenerator.GetBytes(SSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(psw, salt,Iterations,_hashAlgorithmName,KSize);

            return string.Join(SDelimeter,Convert.ToBase64String(salt),Convert.ToBase64String(hash));
        }

        public static bool Validat (string pswHash, string psw)
        {
            var pwdElements =pswHash.Split(SDelimeter);
            var salt = Convert.FromBase64String(pwdElements[0]);
            var hash = Convert.FromBase64String(pwdElements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(psw, salt, Iterations, _hashAlgorithmName, KSize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }
    }
}
