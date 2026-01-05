using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SnykTestApp
{
    class Program
    {
        private static string connectionString = "Server=myserver;Database=mydb;User Id=admin;Password=password123;";

        static void Main(string[] args)
        {
            Console.WriteLine("Snyk Test Application");
            string userInput = Console.ReadLine() ?? "";

            ExecuteQuery(userInput);
            ReadFile(userInput);

            var hash = HashPassword("mypassword");
            Console.WriteLine($"Hash: {hash}");
        }

        // SQL Injection - user input in query
        static void ExecuteQuery(string userName)
        {
            using var connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Users WHERE Name = '" + userName + "'";
            using var command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteReader();
        }

        // Path traversal - reading file with user input
        static void ReadFile(string fileName)
        {
            string path = Path.Combine("C:/data", fileName);
            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }

        // Weak MD5 hash
        static string HashPassword(string password)
        {
            using var md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes);
        }
    }
}
