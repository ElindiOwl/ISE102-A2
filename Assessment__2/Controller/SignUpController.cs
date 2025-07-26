using Assessment__2.Service;
using Assessment__2.Validator;
using Assessment__2.Enum;
using Assessment__2.Model;
using Assessment__2.Utility;
using Assessment__2.UI;

namespace Assessment__2.Controller;

// Declaring controller for sign-up business logic with primary constructor
public class SignUpController(
    IUserService userService,
    IValidationMessageService validationMessages,
    UserValidator userValidator,
    IUserMenuUi userMenuUi,
    InputHelper inputHelper
)
{
    // Declaring private fields for logic
    private readonly IUserService _userService = userService;
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly UserValidator _userValidator = userValidator;
    private readonly IUserMenuUi _userMenuUi = userMenuUi;
    private readonly InputHelper _inputHelper = inputHelper;

    // Encapsulating sign-up process logic
    public void RunSignUp()
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
            Console.WriteLine("\nRegistration successful!");
            _userMenuUi.RunUserMenu();
        }
        else
        {
            Console.WriteLine(_validationMessages.GetMessage(ValidationError.RegistrationFailed));
        }
    }
} 