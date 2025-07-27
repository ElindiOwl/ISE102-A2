using System;
using System.Text.RegularExpressions;

// Name: Shanel Dias
// Student ID: A00178412

class Program
{
    static void Main()
    {
        const string correctPassword = "Passw0rd!";
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("Enter password: ");
            string input = Console.ReadLine();

            if (!IsValidPassword(input))
            {
                Console.WriteLine("Invalid password format. Must contain letters, digits, and at least one special character.");
                continue; // Does NOT count as a try
            }

            if (input == correctPassword)
            {
                Console.WriteLine("Welcome!");
                return;
            }
            else
            {
                attempts++;
                Console.WriteLine($"Incorrect password. Attempts left: {3 - attempts}");
            }
        }

        Console.WriteLine("Goodbye");
    }

    static bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        bool hasLetter = Regex.IsMatch(password, @"[a-zA-Z]");
        bool hasDigit = Regex.IsMatch(password, @"\d");
        bool hasSpecial = Regex.IsMatch(password, @"[^a-zA-Z0-9]");

        return hasLetter && hasDigit && hasSpecial;
    }
}

