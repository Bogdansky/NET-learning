using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Task1
{
    // Try to review and optimize the code to improve the performance of the method. Do not reduce iterations’ number.
    internal class Program
    {
        static void Main(string[] args)
        {
            var password = "VerySTR0nGP@$Sw0rd1234321mklsfamlkklsnkfnsdknvkjnu3992080jiocvnmkjJ@UHiuo3j9iq89482988";

            var salt = new byte[16] { 80, 190, 253, 197, 173, 213, 232, 21, 4, 213, 251, 20, 195, 237, 76, 85};
            

            var methods = new Methods();

            var hash2 = methods.GeneratePasswordHashUsingSaltOrigin(password, salt);
            var hash1 = methods.GeneratePasswordHashUsingSaltOptimized(password, salt);

            Console.WriteLine("Bye, World! Guess what, {0}", hash1 == hash2);
        }
    }

    class Methods
    {
        public string GeneratePasswordHashUsingSaltOrigin(string passwordText, byte[] salt)
        {
            // can be constant
            var iterate = 10000;

            // obsolete https://learn.microsoft.com/ru-ru/dotnet/api/system.security.cryptography.rfc2898derivebytes?view=net-7.0#constructors
            // can use using
            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            // not sure, can be replaced by circle copying
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public string GeneratePasswordHashUsingSaltOptimized(string passwordText, byte[] salt)
        {
            const int iterate = 10000;

            byte[] hashBytes = new byte[36];

            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate, HashAlgorithmName.SHA1))
            {
                byte[] hash = pbkdf2.GetBytes(20);

                for(int i = 0, y = -16; i < hashBytes.Length; i++, y++)
                {
                    hashBytes[i] = y < 0 ? salt[i] : hash[y];
                }
            }

            var passwordHash = Convert.ToBase64String(new ReadOnlySpan<byte>(hashBytes), Base64FormattingOptions.None);

            return passwordHash;
        }
    }
}