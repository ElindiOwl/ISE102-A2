using Assessment__2.Models;
using Assessment__2.Services;
using Assessment__2.Validators;
using Assessment__2.Configuration;
using Assessment__2.Enum;

namespace Assessment__2.UI;

public class BankUI(
    IAuthenticationService authService,
    IUserService userService,
    IValidationMessageService validationMessages,
    ValidationConfig validationConfig
) : IBankUI
{
    private readonly IAuthenticationService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly ValidationConfig _validationConfig = validationConfig;
    private readonly UserValidator _userValidator = new(validationConfig, validationMessages);

    public void RunBankSystem()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n=== Bank System ===");
                Console.WriteLine("1. Login");
                Console.WriteLine("2. Sign Up");
                Console.WriteLine("0. Exit");
                Console.Write("\nEnter your choice: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        HandleLogin();
                        break;
                    case "2":
                        HandleSignUp();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(_validationMessages.GetMessage(ValidationError.UnexpectedError));
            }
        }
    }

    public void HandleLogin()
    {
        Console.WriteLine("\n=== Login ===");
        
        while (true)
        {
            if (_authService.IsLoginAttemptsExceeded())
            {
                var exceededResult = _authService.Login("", "");
                Console.WriteLine(exceededResult.Message);
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter Username: ");
            string? username = Console.ReadLine()?.Trim();
            
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine()?.Trim();

            var result = _authService.Login(username ?? "", password ?? "");
            
            if (result.Error == LoginError.EmptyCredentials)
            {
                Console.WriteLine(result.Message);
                continue;
            }

            Console.WriteLine(result.Message);

            if (result.IsSuccess && result.User != null)
            {
                Console.WriteLine("Login successful. You are now in the main screen.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }
            
            if (result.Error == LoginError.InvalidCredentials)
            {
                Console.WriteLine($"Remaining attempts: {result.RemainingAttempts}");
            }
            else if (result.Error == LoginError.MaxAttemptsExceeded)
            {
                Console.WriteLine("Press any key to return to main menu...");
                Console.ReadKey();
                return;
            }
        }
    }

    private static string PromptForValidString(string prompt, Func<string, ValidationResult> validate)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim() ?? "";
            var result = validate(input);
            if (!result.IsValid)
            {
                Console.WriteLine(result.Error);
                continue;
            }
            return input;
        }
    }

    private static int PromptForValidInt(string prompt, Func<int, ValidationResult> validate, string invalidMsg)
    {
        while (true)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();
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

    public void HandleSignUp()
    {
        Console.WriteLine("\n=== Sign Up ===");
        
        string username;
        while (true)
        {
            username = PromptForValidString("Enter Username: ", _userValidator.ValidateUsername);
            if (!_userService.IsUsernameUnique(username))
            {
                Console.WriteLine(_validationMessages.GetMessage(ValidationError.UsernameExists));
                continue;
            }
            break;
        }
        
        string email = PromptForValidString("Enter Email: ", _userValidator.ValidateEmail);
        
        int age = PromptForValidInt(
            "Enter Age: ",
            _userValidator.ValidateAge,
            _validationMessages.GetMessage(ValidationError.AgeInvalid)
        );
        
        string phone = PromptForValidString("Enter Phone: ", _userValidator.ValidatePhone);
        
        string password = PromptForValidString("Enter Password: ", _userValidator.ValidatePassword);
        
        var newUser = new User
        {
            Username = username,
            Email = email,
            Age = age,
            Phone = phone,
            Password = password
        };
        
        if (_userService.RegisterUser(newUser))
        {
            Console.WriteLine("\nRegistration successful!");
            
            var testResult = _authService.Login(newUser.Username, newUser.Password);
            if (testResult.IsSuccess)
            {
                Console.WriteLine("Menu here");
            }
        }
        else
        {
            Console.WriteLine(_validationMessages.GetMessage(ValidationError.RegistrationFailed));
        }
    }
} 