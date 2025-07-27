using System;

class Bank
{
    public void Signup()
    {
        Console.WriteLine("=== Signup Form ===");

        Console.Write("Enter Username: ");
        string username = Console.ReadLine();

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Console.Write("Enter Age: ");
        string ageInput = Console.ReadLine();

        Console.Write("Enter Phone: ");
        string phone = Console.ReadLine();

        Console.Write("Enter Password: ");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) ||
            string.IsNullOrWhiteSpace(ageInput) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Please fill all fields correctly.");
            return;
        }

        if (!int.TryParse(ageInput, out int age))
        {
            Console.WriteLine("Age must be a number.");
            return;
        }

        Console.WriteLine("Signup successful! Welcome, " + username);
    }
}

class Program
{
    static void Main()
    {
        Bank bank = new Bank();
        bank.Signup();
    }
}
