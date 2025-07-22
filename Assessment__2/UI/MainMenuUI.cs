using Assessment__2.Service;
using Assessment__2.Validator;
using Assessment__2.Enum;
using Assessment__2.Model;
using Assessment__2.Utility;

namespace Assessment__2.UI;

public class MainMenuUi(
    IAuthenticationService authService,
    IUserService userService,
    IValidationMessageService validationMessages,
    UserValidator userValidator,
    IUserMenuUi userMenuUi
) : IMainMenuUi
{
    private readonly IAuthenticationService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly UserValidator _userValidator = userValidator;
    private readonly IUserMenuUi _userMenuUi = userMenuUi;

    public void RunMainMenu()
    {
        bool exitMainMenu = false;
        var menu = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("Login", HandleLogin) },
            { "2", ("Sign Up", HandleSignUp) },
            { "0", ("Quit", () => exitMainMenu = true) }
        };
        
        while (!exitMainMenu)
        {
            try
            {
                Console.WriteLine("\n=== Bank AS2 ===");
                
                foreach (var item in menu)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Description}");
                }
                
                string choice = InputHelper.GetInput("Enter your choice: ");
                
                if (!menu.TryGetValue(choice, out var menuItem))
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                
                menuItem.Action();
            }
            catch (Exception exception)
            {
                Console.WriteLine(_validationMessages.GetMessage(ValidationError.UnexpectedError));
                Console.Error.WriteLine(exception);
            }
        }
    }

    private void HandleLogin()
    {
        Console.WriteLine("\n=== Login ===");
        
        while (true)
        {
            if (_authService.IsLoginAttemptsExceeded())
            {
                var lockoutStatus = _authService.GetLockoutStatus();
                
                if (lockoutStatus != null)
                {
                    Console.WriteLine(lockoutStatus.Message);
                    InputHelper.WaitForUser("Press any key to return to main menu...");
                    return;
                }
            }
            string username = InputHelper.GetInput("Enter Username: ");
            
            string password = InputHelper.GetInput("Enter Password: ");
            
            var result = _authService.Login(username, password);
            
            if (result.Error == LoginError.EmptyCredentials)
            {
                Console.WriteLine(result.Message);
                continue;
            }
            
            Console.WriteLine(result.Message);
            
            if (result.IsSuccess && result.User != null)
            {
                _userMenuUi.RunUserMenu();
                return;
            }
            
            if (result.Error == LoginError.InvalidCredentials)
            {
                Console.WriteLine($"Remaining attempts: {result.RemainingAttempts}");
            }
            else if (result.Error == LoginError.MaxAttemptsExceeded)
            {
                InputHelper.WaitForUser("Press any key to return to main menu...");
                return;
            }
        }
    }

    private void HandleSignUp()
    {
        Console.WriteLine("\n=== Sign Up ===");
        
        string username;
        
        while (true)
        {
            username = InputHelper.GetValidString(
                "Enter Username: ", 
                _userValidator.ValidateUsername
                );
            
            if (!_userService.IsUsernameUnique(username))
            {
                Console.WriteLine(_validationMessages.GetMessage(ValidationError.UsernameExists));
                continue;
            }
            break;
        }
        
        string email = InputHelper.GetValidString(
            "Enter Email: ", 
            _userValidator.ValidateEmail
            );
        
        int age = InputHelper.GetValidInt(
            "Enter Age: ",
            _userValidator.ValidateAge,
            _validationMessages.GetMessage(ValidationError.AgeInvalid)
            );
        
        string phone = InputHelper.GetValidString(
            "Enter Phone: ", 
            _userValidator.ValidatePhone
            );
        
        string password = InputHelper.GetValidString(
            "Enter Password: ", 
            _userValidator.ValidatePassword
            );
        
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
                _userMenuUi.RunUserMenu();
            }
        }
        else
        {
            Console.WriteLine(_validationMessages.GetMessage(ValidationError.RegistrationFailed));
        }
    }
} 