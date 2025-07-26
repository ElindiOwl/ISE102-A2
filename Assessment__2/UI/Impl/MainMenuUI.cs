using Assessment__2.Service;
using Assessment__2.Validator;
using Assessment__2.Enum;
using Assessment__2.Model;
using Assessment__2.Utility;

namespace Assessment__2.UI.Impl;

public class MainMenuUi(
    IAuthenticationService authService,
    IUserService userService,
    IValidationMessageService validationMessages,
    UserValidator userValidator,
    IUserMenuUi userMenuUi,
    InputHelper inputHelper
) : IMainMenuUi
{
    private readonly IAuthenticationService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly UserValidator _userValidator = userValidator;
    private readonly IUserMenuUi _userMenuUi = userMenuUi;
    private readonly InputHelper _inputHelper = inputHelper;

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
                
                string choice = _inputHelper.GetInput("Enter your choice: ");
                
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
            string username = _inputHelper.GetInput("Enter Username: ");
            string password = _inputHelper.GetInput("Enter Password: ");
            
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
                _inputHelper.WaitForUser("Press any key to return to main menu...");
                return;
            }
        }
    }

    private void HandleSignUp()
    {
        Console.WriteLine("\n=== Sign Up ===");
        
        string username = _inputHelper.GetValidString(
            "Enter Username: ", 
            _userValidator.ValidateUsername
        );
        
        string email = _inputHelper.GetValidString(
            "Enter Email: ", 
            _userValidator.ValidateEmail
        );
        
        int age = _inputHelper.GetValidInt(
            "Enter Age: ",
            _userValidator.ValidateAge
        );
        
        string phone = _inputHelper.GetValidString(
            "Enter Phone: ", 
            _userValidator.ValidatePhone
        );
        
        string password = _inputHelper.GetValidString(
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

            _userMenuUi.RunUserMenu();
        }
        else
        {
            Console.WriteLine(_validationMessages.GetMessage(ValidationError.RegistrationFailed));
        }
    }
} 