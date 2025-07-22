using Assessment__2.Model;

namespace Assessment__2.Utility;

public static class InputHelper
{
    
    public static string GetValidString(string text, Func<string, ValidationResult> validate)
    {
        while (true)
        {
            var input = GetInput(text);
            var result = validate(input);
            if (!result.IsValid)
            {
                Console.WriteLine(result.Error);
                continue;
            }
            return input;
        }
    }

    public static int GetValidInt(string text, Func<int, ValidationResult> validate, string invalidMsg)
    {
        while (true)
        {
            var input = GetInput(text);
            if (!int.TryParse(input, out int value))
            {
                Console.WriteLine(invalidMsg);
                continue;
            }
            var result = validate(value);
            if (!result.IsValid)
            {
                Console.WriteLine(result.Error);
                continue;
            }
            return value;
        }
    }

    public static string GetInput(string text)
    {
        Console.Write(text);
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

    public static void WaitForUser(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
} 