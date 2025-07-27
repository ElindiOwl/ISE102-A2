using Assessment__2.Service;
using Assessment__2.Enum;
using Assessment__2.Utility;
using Assessment__2.Controller;

namespace Assessment__2.UI.Impl;

// Declaring class implementing main menu UI with constructor injection
public class MainMenuUi(
    ISignUpMessageService signUpMessages,
    LoginController loginController,
    SignUpController signUpController,
    MenuHelper menuHelper
) : IMainMenuUi
{
    // Declaring private field for logic
    private readonly ISignUpMessageService _signUpMessages = signUpMessages;
    private readonly LoginController _loginController = loginController;
    private readonly SignUpController _signUpController = signUpController;
    private readonly MenuHelper _menuHelper = menuHelper;

    // Implementing method for running main menu
    public void RunMainMenu()
    {
        // Declaring local menu flag for menu exit control
        var exitMainMenu = false;
        
        // Creating main menu options dictionary
        var menuOptions = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("Login", _loginController.RunLogin) },
            { "2", ("Sign Up", _signUpController.RunSignUp) },
            { "0", ("Quit", () => exitMainMenu = true) }
        };

        try
        {
            // Running main menu with exit condition
            _menuHelper.RunMenu("Bank AS2", menuOptions, () => exitMainMenu);
        }
        catch (Exception exception)
        {
            // Displaying unexpected error message for user
            Console.WriteLine(_signUpMessages.GetMessage(SignUpError.RegistrationFailed));
            // Logging exception details for developer
            Console.Error.WriteLine(exception);
        }
    }
} 