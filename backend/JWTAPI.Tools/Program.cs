using System;
using JWTHLAPI.Helpers;

namespace JWTAPI.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Password Hash Generator ===");

            string passwordHash;
            string passwordSalt;

            PasswordHasher.CreatePasswordHash(
                "Admin@123",
                out passwordHash,
                out passwordSalt
            );

            Console.WriteLine("\nPasswordHash:");
            Console.WriteLine(passwordHash);

            Console.WriteLine("\nPasswordSalt:");
            Console.WriteLine(passwordSalt);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}