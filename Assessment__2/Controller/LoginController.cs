// Daniil Abkhalimov, studentId: A00177578

using Assessment__2.Service;
using Assessment__2.Enum;
using Assessment__2.Utility;
using Assessment__2.UI;
using Assessment__2.Validator;

namespace Assessment__2.Controller;

// Declaring controller for login business logic with primary constructor
public class LoginController(
    IAuthenticationService authService,
    IUserMenuUi userMenuUi,
    InputHelper inputHelper,
    LoginValidator loginValidator
)
{
    // Declaring private fields for logic
    private readonly IAuthenticationService _authService = authService;
    private readonly IUserMenuUi _userMenuUi = userMenuUi;
    private readonly InputHelper _inputHelper = inputHelper;
    private readonly LoginValidator _loginValidator = loginValidator;

    // Encapsulating login process logic
    public void RunLogin()
    {
        // Displaying login header
        Console.WriteLine("\n=== Login ===");
        while (true)
        {
            // Getting validated username input
            string username = _inputHelper.GetValidString(
                "Enter Username: ", 
                _loginValidator.ValidateUsername
                );
            
            // Getting validated password input
            string password = _inputHelper.GetValidString(
                "Enter Password: ", 
                _loginValidator.ValidatePassword
                );
            
            // Attempting user authentication
            var result = _authService.Login(username, password);
            
            // Displaying result message
            Console.WriteLine(result.Message);
            
            // Handling successful login
            if (result.IsSuccess && result.User != null)
            {
                _userMenuUi.RunUserMenu();
                return;
            }
            
            // Handling invalid credentials
            if (result.Error == LoginError.InvalidCredentials)
            {
                Console.WriteLine($"Remaining attempts: {result.RemainingAttempts}");
            }
            
            // Handling exceeded attempts
            else if (result.Error == LoginError.MaxAttemptsExceeded)
            {
                _inputHelper.WaitForUser("Press any key to return to main menu...");
                return;
            }
        }
    }
} 