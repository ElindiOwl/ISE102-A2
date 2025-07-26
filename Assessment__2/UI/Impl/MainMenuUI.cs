using Assessment__2.Service;
using Assessment__2.Enum;
using Assessment__2.Utility;
using Assessment__2.Controller;

namespace Assessment__2.UI.Impl;

// Declaring class implementing main menu UI with constructor injection
public class MainMenuUi(
    IValidationMessageService validationMessages,
    InputHelper inputHelper,
    LoginController loginController,
    SignUpController signUpController
) : IMainMenuUi
{
    // Declaring private field for logic
    private readonly IValidationMessageService _validationMessages = validationMessages;
    private readonly InputHelper _inputHelper = inputHelper;
    private readonly LoginController _loginController = loginController;
    private readonly SignUpController _signUpController = signUpController;

    // Implementing method for running main menu
    public void RunMainMenu()
    {
        // Declaring flag for menu exit control
        bool exitMainMenu = false;
        // Creating menu options dictionary
        var menu = new Dictionary<string, (string Description, Action Action)>
        {
            { "1", ("Login", _loginController.RunLogin) },
            { "2", ("Sign Up", _signUpController.RunSignUp) },
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
} 