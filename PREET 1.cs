{//Student ID- A00193960
 //Name-Manpreet kaur}
using System;

class Program
{
    // Main method: the entry point of the program
    static void Main(string[] args)
    {
        int choice; // Variable to store user's menu selection

        // Loop continues until the user chooses to exit (choice 4)
        do
        {
            // ======================= MENU DISPLAY ============================
            // Displaying menu options to the user
            Console.WriteLine("============== MAIN MENU ==============");
            Console.WriteLine("1. Display Welcome Message");          // Option 1
            Console.WriteLine("2. Show Current Date and Time");     // Option 2
            Console.WriteLine("3. Perform Addition");               // Option 3
            Console.WriteLine("4. Exit");                           // Option 4
            Console.WriteLine("=======================================");
            Console.Write("Enter your choice (1-4): ");             // Asking user for input

            // Try to convert user input to an integer
            bool isValid = int.TryParse(Console.ReadLine(), out choice);

            // ===================== VALIDATION ===============================
            // If input is not a valid number, show an error and re-display menu
            if (!isValid)
            {
                Console.WriteLine("❌ Invalid input. Please enter a number between 1 and 4.\n");
                continue; // Skip rest of loop and start again
            }

            // ==================== CHOICE TRIGGERING =========================
            // Perform actions based on user's menu choice
            switch (choice)
            {
                case 1:
                    // ✅ Case 1: Display welcome message
                    Console.WriteLine("\n👉 Hello! Welcome to our group project console app!\n");
                    break;

                case 2:
                    // ✅ Case 2: Display current date and time
                    Console.WriteLine($"\n📅 Current Date and Time: {DateTime.Now}\n");
                    break;

                case 3:
                    // ✅ Case 3: Perform addition of two numbers

                    // Ask for first number
                    Console.Write("Enter first number: ");
                    double num1 = Convert.ToDouble(Console.ReadLine());

                    // Ask for second number
                    Console.Write("Enter second number: ");
                    double num2 = Convert.ToDouble(Console.ReadLine());

                    // Calculate sum
                    double sum = num1 + num2;

                    // Display result
                    Console.WriteLine($"\n✅ The sum is: {sum}\n");
                    break;

                case 4:
                    // ✅ Case 4: Exit the program
                    Console.WriteLine("\n👋 Exiting program. Goodbye!");
                    break;

                default:
                    // ❌ If choice is not between 1 and 4
                    Console.WriteLine("❌ Invalid choice. Please select between 1 and 4.\n");
                    break;
            }

        } while (choice != 4); // Loop condition: runs until user selects "Exit"
    }
}
