using System.Security.Cryptography;

namespace Task1
{
    // Try to review and optimize the code to improve the performance of the method. Do not reduce iterations’ number.
    internal class Program
    {
        static void Main(string[] args)
        {
            var password = "VerySTR0nGP@$Sw0rd1234321mklsfamlkklsnkfnsdknvkjnu3992080jiocvnmkjJ@UHiuo3j9iq89482988";

            var salt = new byte[16] { 80,
190,
253,
197,
173,
213,
232,
21,
4,
213,
251,
20,
195,
237,
76,
85
};
            

            var methods = new Methods();

            _ = methods.GeneratePasswordHashUsingSaltOptimized(password, salt);
            //_ = methods.GeneratePasswordHashUsingSaltOrigin(password, salt);

            Console.WriteLine("Bye, World!");
        }
    }

    class Methods
    {
        public string GeneratePasswordHashUsingSaltOrigin(string passwordText, byte[] salt)
        {
            var iterate = 10000;

            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public string GeneratePasswordHashUsingSaltOptimized(string passwordText, byte[] salt)
        {
            const int Iterate = 10000;

            var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, Iterate);

            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];

            var index = 0;
            for(; index < 16; index++)
            {
                hashBytes[index] = salt[index];
            }

            for(var secondIndex = 0; secondIndex < 20; secondIndex++)
            {
                hashBytes[secondIndex + index] = hash[secondIndex];
            }

            var passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }
    }
}