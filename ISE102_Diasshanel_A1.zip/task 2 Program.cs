using System;

// Name: Shanel Dias
// Student ID: A00178412

class Program
{
    static void Main()
    {
        Console.Write("Enter the score (0 to 100): ");
        string input = Console.ReadLine();

        bool success = int.TryParse(input, out int score);

        if (!success)
        {
            Console.WriteLine("Invalid input. Please enter a number.");
            return;
        }

        if (score >= 85 && score <= 100)
            Console.WriteLine("Grade: High Distinction");
        else if (score >= 75 && score <= 84)
            Console.WriteLine("Grade: Distinction");
        else if (score >= 65 && score <= 74)
            Console.WriteLine("Grade: Credit");
        else if (score >= 55 && score <= 64)
            Console.WriteLine("Grade: Pass");
        else if (score >= 0 && score <= 54)
            Console.WriteLine("Grade: Fail");
        else
            Console.WriteLine("Invalid score.");
    }
}

