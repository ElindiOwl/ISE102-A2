using System;

namespace ISE102_A2
{
    class Program
    {
        private static string storedUsername = "Joey";
        private static string storedEmail = "Joey@gmail.com";
        private static string storedPassword = "joey123";
        private static string currentUsername = "";
        
        private static void Main()
        {
            HomeScreen();
        }
        
        public static void HomeScreen()
        {
            Console.WriteLine("Welcome to abc Bank");
            Console.WriteLine("1 : Login");
            Console.WriteLine("2 : Signup");
            Console.WriteLine("3 : Quit");
            Console.Write("Select Option: ");
            string option = Console.ReadLine();
            
            if (option == "1")
            {
                Login();
            }
            else if (option == "2")
            {
                Signup();
            }
            else if (option == "3")
            {
                Console.WriteLine("Exit Successfully");
            }
        }
        
        public static void Login()
        {
            for (int maxattempt = 3; maxattempt > 0; maxattempt--)
            {
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                // Compare with stored credentials
                if (email == storedEmail && password == storedPassword)
                {
                    currentUsername = storedUsername; // Set current user
                    Console.WriteLine("Login successful!");
                    Homepage();
                    return;
                }
                else
                {
                    Console.WriteLine($"Invalid credentials. {maxattempt - 1} attempts remaining.");
                }
            }
            Console.WriteLine("Too many failed attempts. Goodbye");
            return;
        }
        
        public static void Signup()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();
            
            // Update stored credentials
            storedUsername = username;
            storedEmail = email;
            storedPassword = password;
            
            Console.WriteLine("Signup successful! Please login.");
            HomeScreen();
        }
        
        public static void Homepage()
        {
            Console.WriteLine($"Welcome {currentUsername}");
            Console.WriteLine("1 : View Balance");
            Console.WriteLine("2 : Deposit");
            Console.WriteLine("3 : Withdraw");
            Console.WriteLine("4 : Transfer");
            Console.WriteLine("5 : Quit");
            Console.Write("Select Option: ");
            string option = Console.ReadLine();

            if (option == "1")
            {
                Console.WriteLine("View Balance selected");
            }
            else if (option == "2")
            {
                Console.WriteLine("Deposit selected");
            }
            else if (option == "3")
            {
                Console.WriteLine("Withdraw selected");
            }
            else if (option == "4")
            {
                Console.WriteLine("Transfer selected");
            }
            else if (option == "5")
            {
                HomeScreen();
            }
        }
    }
}
