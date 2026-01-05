using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SnykTestApp
{
    class Program
    {
        // Hardcoded credentials (security issue)
        private static string connectionString = "Server=myserver;Database=mydb;User Id=admin;Password=password123;";

        static void Main(string[] args)
        {
            Console.WriteLine("Snyk Test Application");

            string userInput = Console.ReadLine() ?? "";

            // SQL Injection vulnerability
            ExecuteQuery(userInput);

            // Path traversal vulnerability
            ReadFile(userInput);

            // Weak cryptography
            var hash = HashPassword("mypassword");
            Console.WriteLine($"Hash: {hash}");

            Console.WriteLine("Done!");
        }

        // SQL Injection - concatenating user input directly
        static void ExecuteQuery(string userName)
        {
            string query = "SELECT * FROM Users WHERE Name = ' + userName + '";
            Console.WriteLine($"Query: {query}");
        }

        // Path traversal vulnerability
        static void ReadFile(string fileName)
        {
            string path = Path.Combine("C:/data", fileName);
            Console.WriteLine($"Reading file: {path}");
        }

        // Using weak MD5 hash
        static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }

        // Insecure random number generator
        static int GenerateToken()
        {
            Random random = new Random();
            return random.Next();
        }
    }
}
