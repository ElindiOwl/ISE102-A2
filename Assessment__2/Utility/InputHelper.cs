using Assessment__2.Model;
using Assessment__2.Service;
using Assessment__2.Enum;

namespace Assessment__2.Utility;

public class InputHelper(ISystemMessageService systemMessages)
{
    private readonly ISystemMessageService _systemMessages = systemMessages;
    
    public string GetValidString(string text, Func<string, ValidationResult> validate)
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

    public int GetValidInt(string text, Func<int, ValidationResult> validate)
    {
        while (true)
        {
            var input = GetInput(text);
            if (!int.TryParse(input, out int value))
            {
                Console.WriteLine(_systemMessages.GetMessage(SystemError.InvalidIntegerInput));
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

    public string GetInput(string text)
    {
        Console.Write(text);
        return (Console.ReadLine() ?? string.Empty).Trim();
    }

    public void WaitForUser(string message)
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
} 