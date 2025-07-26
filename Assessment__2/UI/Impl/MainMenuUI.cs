using Assessment__2.Service;
using Assessment__2.Validator;
using Assessment__2.Enum;
using Assessment__2.Model;
using Assessment__2.Utility;

namespace Assessment__2.UI.Impl;

// Declaring class implementing main menu UI with constructor injection
public class MainMenuUi(
    IAuthenticationService authService,
    IUserService userService,
    IValidationMessageService validationMessages,
    UserValidator userValidator,
    IUserMenuUi userMenuUi,
    InputHelper inputHelper
) : IMainMenuUi
{
    // Declaring private field for logic
    private readonly IAuthenticationService _authService = authService;
    private readonly IUserService _userService = userService;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly UserValidator _userValidator = userValidator;
    private readonly IUserMenuUi _userMenuUi = userMenuUi;
    private readonly InputHelper _inputHelper = inputHelper;

    // Implementing method for running main menu
    public void RunMainMenu()
    {
        // Declaring flag for menu exit control
        bool exitMainMenu = false;
        // Creating menu options dictionary
        var menu = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("Login", HandleLogin) },
            { "2", ("Sign Up", HandleSignUp) },
            { "0", ("Quit", () => exitMainMenu = true) }
        };
        
        // Running menu loop until exit
        while (!exitMainMenu)
        {
            try
            {
                // Displaying menu header
                Console.WriteLine("\n=== Bank AS2 ===");
                
                // Displaying menu options
                foreach (var item in menu)
                {
                    Console.WriteLine($"{item.Key}. {item.Value.Description}");
                }
                
                // Getting user choice
                string choice = _inputHelper.GetInput("Enter your choice: ");
                
                // Validating menu choice
                if (!menu.TryGetValue(choice, out var menuItem))
                {
                    // Displaying invalid choice message
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }
                
                // Executing selected menu action
                menuItem.Action();
            }
            catch (Exception exception)
            {
                // Displaying unexpected error message for user 
                Console.WriteLine(_validationMessages.GetMessage(ValidationError.UnexpectedError));
                // Logging exception details for developer
                Console.Error.WriteLine(exception);
            }
        }
    }

    // Implementing method for handling login process
    private void HandleLogin()
    {
        // Displaying login header
        Console.WriteLine("\n=== Login ===");
        
        // Running login loop
        while (true)
        {
            // Getting username input
            string username = _inputHelper.GetInput("Enter Username: ");
            // Getting password input
            string password = _inputHelper.GetInput("Enter Password: ");
            
            // Attempting user authentication
            var result = _authService.Login(username, password);
            
            // Handling empty credentials error
            if (result.Error == LoginError.EmptyCredentials)
            {
                // Displaying error message
                Console.WriteLine(result.Message);
                continue;
            }
            
            // Displaying result message
            Console.WriteLine(result.Message);
            
            // Handling successful login
            if (result.IsSuccess && result.User != null)
            {
                // Running user menu
                _userMenuUi.RunUserMenu();
                return;
            }
            
            // Handling invalid credentials
            if (result.Error == LoginError.InvalidCredentials)
            {
                // Displaying remaining attempts
                Console.WriteLine($"Remaining attempts: {result.RemainingAttempts}");
            }
            // Handling exceeded attempts
            else if (result.Error == LoginError.MaxAttemptsExceeded)
            {
                // Waiting for user input before returning to main menu
                _inputHelper.WaitForUser("Press any key to return to main menu...");
                return;
            }
        }
    }

    // Implementing method for handling sign up process
    private void HandleSignUp()
    {
        // Displaying sign up header
        Console.WriteLine("\n=== Sign Up ===");
        
        // Getting validated username input
        string username = _inputHelper.GetValidString(
            "Enter Username: ", 
            _userValidator.ValidateUsername
        );
        
        // Getting validated email input
        string email = _inputHelper.GetValidString(
            "Enter Email: ", 
            _userValidator.ValidateEmail
        );
        
        // Getting validated age input
        int age = _inputHelper.GetValidInt(
            "Enter Age: ",
            _userValidator.ValidateAge
        );
        
        // Getting validated phone input
        string phone = _inputHelper.GetValidString(
            "Enter Phone: ", 
            _userValidator.ValidatePhone
        );
        
        // Getting validated password input
        string password = _inputHelper.GetValidString(
            "Enter Password: ", 
            _userValidator.ValidatePassword
        );
        
        // Creating new user object
        var newUser = new User
        {
            Username = username,
            Email = email,
            Age = age,
            Phone = phone,
            Password = password
        };
        
        // Attempting user registration
        if (_userService.RegisterUser(newUser))
        {
            // Displaying success message
            Console.WriteLine("\nRegistration successful!");

            // Running user menu
            _userMenuUi.RunUserMenu();
        }
        else
        {
            // Displaying registration failure message
            Console.WriteLine(_validationMessages.GetMessage(ValidationError.RegistrationFailed));
        }
    }
} 