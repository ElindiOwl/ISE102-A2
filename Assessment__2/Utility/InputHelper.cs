// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Model;
using Assessment__2.Service;
using Assessment__2.Enum;

namespace Assessment__2.Utility;

// Declaring class for input helper with constructor injection
public class InputHelper(ISystemMessageService systemMessages)
{
    // Declaring private field for system message service dependency
    private readonly ISystemMessageService _systemMessages = systemMessages;
    
    // Implementing method for getting validated string input
    public string GetValidString(string text, Func<string, ValidationResult> validate)
    {
        // Running validation loop until valid input
        while (true)
        {
            // Getting user input
            var input = GetInput(text);
            // Validating input using provided function
            var result = validate(input);
            // Checking if validation failed
            if (!result.IsValid)
            {
                // Displaying validation error
                Console.WriteLine(result.Error);
                continue;
            }
            // Returning valid input
            return input;
        }
    }

    // Implementing method for getting validated integer input
    public int GetValidInt(string text, Func<int, ValidationResult> validate)
    {
        // Running validation loop until valid input
        while (true)
        {
            // Getting user input
            var input = GetInput(text);
            // Attempting to parse input as integer
            if (!int.TryParse(input, out int value))
            {
                // Displaying invalid integer error
                Console.WriteLine(_systemMessages.GetMessage(SystemError.InvalidIntegerInput));
                continue;
            }
            // Validating parsed integer using provided function
            var result = validate(value);
            // Checking if validation failed
            if (!result.IsValid)
            {
                // Displaying validation error
                Console.WriteLine(result.Error);
                continue;
            }
            // Returning valid integer
            return value;
        }
    }

    // Implementing method for getting basic string input
    public string GetInput(string text)
    {
        // Displaying prompt text
        Console.Write(text);
        // Reading and trimming user input
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

    // Implementing method for waiting for user input
    public void WaitForUser(string message)
    {
        // Displaying message
        Console.WriteLine(message);
        // Waiting for key press
        Console.ReadKey();
    }
} 